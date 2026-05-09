using Cysharp.Threading.Tasks;
using Game.Battle.Character;
using Given.Manager;
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

    public virtual void Initialise()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentMaxStamina = maxStamina;


    }
    void RoundStart ()
    {
        
    }
    protected async UniTask RollDice ()
    {
        UniTask<int>[] tasks = new UniTask<int>[DiceToRoll.Length];

        for (int i = 0; i <DiceToRoll.Length; i += 1) {
       Dice dice = DiceManager.Instance.CreateDice(DiceToRoll[i], transform.position.x < 0, diceGlow, textGlow);

            tasks[i] = dice.Roll(dice.transform.forward);
        
        }
        
            int[] num = await UniTask.WhenAll(tasks);

        int sum  = 0;

               for (int i = 0; i < num.Length; i += 1)
        {
            sum += num[i];

        }
        Debug.Log($"Player Rolled  {sum}", gameObject);
        Debug.Log($"Mean {num.AverageMean()}" );
        Debug.Log($"Medium {num.AverageMedian()}");
        Debug.Log($"Mode {num.AverageMode()}");
        GraphManager.Instance?.RegisterRoll(GetBattleDice(), sum);
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

    public void TakeDamage()
    {
        
    }

    public EDiceType[] GetBattleDice()
    {
        return DiceToRoll;
    }
}
