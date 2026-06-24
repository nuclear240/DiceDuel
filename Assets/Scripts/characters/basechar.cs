using Cysharp.Threading.Tasks;
using Game.Battle.Character;
using Given.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public abstract class basechar : MonoBehaviour, IWarrior
{
    protected abstract EDiceType[] DiceToRoll { get; set; }
   [SerializeField] private int maxStamina;
   [SerializeField] private int maxHealth;
   [SerializeField] private Color textGlow = new Color(0,0,0);
   [SerializeField] private Color diceGlow = new Color(0, 1, 1);
   private int currentStamina;
   private int currentHealth;
   private int currentMaxStamina;
    protected AudioSource autosourse;
    [SerializeField] protected AnilityBase[] activeAbilitied;
    [SerializeField] private AudioResource hurtSong;
    [SerializeField] private AudioResource blov;
    [SerializeField] private AudioResource help;
    public List<AbilityData> abilities = new List<AbilityData>();

    public IWarrior target { get; set; }
    public int Shield { get; set; }

    public virtual void Initialise()
    {
        ValidateAssignments();

        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentMaxStamina = maxStamina;
        autosourse = GetComponent<AudioSource>();

        Debug.Log($"Initialized: {currentHealth}, {currentStamina}, {currentMaxStamina}");

    }

    // Hard runtime check. Logs errors (with this GameObject as context, so clicking
    // the log selects the offender) the moment a character tries to enter battle
    // while misconfigured.
    protected virtual void ValidateAssignments()
    {
        if (maxHealth <= 0)
            Debug.LogError($"[{name}] maxHealth is {maxHealth}. Health must be > 0.", gameObject);

        if (maxStamina <= 0)
            Debug.LogError($"[{name}] maxStamina is {maxStamina}. Stamina must be > 0.", gameObject);

        if (activeAbilitied == null || activeAbilitied.Length == 0)
            Debug.LogError($"[{name}] activeAbilitied is empty. This character will roll nothing.", gameObject);
        else if (activeAbilitied.Any(a => a == null))
            Debug.LogError($"[{name}] activeAbilitied contains a null entry.", gameObject);

        if (DiceToRoll == null || DiceToRoll.Length == 0)
            Debug.LogError($"[{name}] DiceToRoll is empty. There are no dice to roll.", gameObject);

        if (GetComponent<AudioSource>() == null)
            Debug.LogError($"[{name}] No AudioSource component found. Heal/TakeDamage will throw.", gameObject);

        if (hurtSong == null) Debug.LogWarning($"[{name}] hurtSong is unassigned.", gameObject);
        if (blov == null)     Debug.LogWarning($"[{name}] blov is unassigned.", gameObject);
        if (help == null)     Debug.LogWarning($"[{name}] help is unassigned.", gameObject);
    }

#if UNITY_EDITOR
    // Editor-time check: fires whenever a value changes in the inspector, so a bad
    // config is flagged before you ever press play.
    protected virtual void OnValidate()
    {
        if (maxHealth <= 0)
            Debug.LogWarning($"[{name}] maxHealth is {maxHealth}. It should be greater than 0.", gameObject);

        if (maxStamina <= 0)
            Debug.LogWarning($"[{name}] maxStamina is {maxStamina}. It should be greater than 0.", gameObject);

        if (activeAbilitied == null || activeAbilitied.Length == 0)
            Debug.LogWarning($"[{name}] activeAbilitied is empty. This character will roll nothing.", gameObject);

        if (DiceToRoll == null || DiceToRoll.Length == 0)
            Debug.LogWarning($"[{name}] DiceToRoll is empty. There are no dice to roll.", gameObject);
    }
#endif

    void RoundStart ()
    {
        
    }
    public async UniTask RollDice ()
    {
        for (int j = 0; j < abilities.Count; ++ j)
        {
            AbilityData Ability = abilities[j];


            UniTask<int>[] tasks = new UniTask<int>[Ability.dicee.Length];

            for (int i = 0; i < Ability.dicee.Length; i += 1)
            {
                Dice dice = DiceManager.Instance.CreateDice(Ability.dicee[i], transform.position.x < 0, Ability.bility.Color, textGlow);

                tasks[i] = dice.Roll(dice.transform.forward);

            }

            int[] num = await UniTask.WhenAll(tasks);

            int sum = 0;

            for (int i = 0; i < num.Length; i += 1)
            {
                sum += num[i];

            }
            Debug.Log($"Player Rolled for {Ability.bility.name} with {sum}", gameObject);
            //Debug.Log($"Mean {num.AverageMean()}" );
            //Debug.Log($"Medium {num.AverageMedian()}");
            //Debug.Log($"Mode {num.AverageMode()}");
            GraphManager.Instance?.RegisterRoll(Ability.dicee, sum);
            Ability.value = sum;
            abilities[j] = Ability;
           
        }
       abilities =  abilities.OrderBy(a => a.bility.AbilityPriority).ToList();

        for (int j = 0; j < abilities.Count; ++j)
        {
            AbilityData Ability = abilities[j];
            await Ability.bility.StartAbility(Ability, target);
        }
        abilities.Clear();
            
        Debug.Log($"Rolled Dice for {gameObject.name}", gameObject);

    }
    void RoundEnd ()
    {


    }
   

    public void EndRound()
    {
        
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public abstract UniTask DewTurn();
  

    public void StartRound()
    {
    
    }

    public void TakeDamage(int value)
    {
        // first, it checks if the health is > 0.
        // Then it also checks if we got hit and how much damage was done. if health =< 0, the player dies.
        


        if (value <= 0) return;

        int DamageToFlesh = Shield - value;

        if (!IsAlive())
        {
            Die();
        }

        if (DamageToFlesh >= 0)
        {
            // attack was blocked. Play sound effex + particled
            Shield -= value;
            autosourse.resource = blov;
            autosourse.Play();
        }
        else
        {
            //attack was NOT blocked
            currentHealth = currentHealth + Shield - DamageToFlesh;
            Shield = 0;
            autosourse.resource = hurtSong;
            autosourse.Play();
        }
    }

    protected virtual void Die()
    {
        // cs2 ragdoll gif + play sound
        Debug.Log("weded", gameObject);
    }

    public EDiceType[] GetBattleDice()
    {
        return DiceToRoll;
    }

    public void Heal(int value)
    {
        // heal sum helth + play tf2 
        currentHealth = currentHealth + value;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        autosourse.resource = help;
        autosourse.Play();
    }


    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public int shield => Shield;
}