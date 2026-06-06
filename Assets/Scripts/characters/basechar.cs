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
   [SerializeField] protected EDiceType[] DiceToRoll;
   [SerializeField] private int maxStamina;
   [SerializeField] private int maxHealth;
   [SerializeField] private int numberOfDice;
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
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentMaxStamina = maxStamina;
        autosourse = GetComponent<AudioSource>();



    }
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



