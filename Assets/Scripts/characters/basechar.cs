using Cysharp.Threading.Tasks;
using Game.Battle.Character;
using Given.Manager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    [SerializeField] protected AnilityBase[] activeAbilitied;
    public List<AbilityData> abilities = new List<AbilityData>();

    public IWarrior target { get; set; }
    public int Shield { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public virtual void Initialise()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentMaxStamina = maxStamina;


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
        return true;
    }

    public abstract UniTask DewTurn();
  

    public void StartRound()
    {
    
    }

    public void TakeDamage(int value)
    {
        
    }

    public EDiceType[] GetBattleDice()
    {
        return DiceToRoll;
    }

    public int Heal(int value)
    {
        throw new System.NotImplementedException();
    }
}
