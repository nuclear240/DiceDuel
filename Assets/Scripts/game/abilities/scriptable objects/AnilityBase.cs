using Cysharp.Threading.Tasks;
using Given.Manager;
using UnityEngine;
public abstract class AnilityBase : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField, ColorUsage(false, true)] private Color color;
    [SerializeField] private bool costsMaxStamina;
    [SerializeField] private int abilityPriority;
    public Sprite Icon => icon;
    public Color Color => color;
    public bool CostsMaxStamina => costsMaxStamina;
    public int AbilityPriority => abilityPriority;

    public UniTask StartAbility(AbilityData DATA, IWarrior enemy)
    {
        if (DATA.value <= 0)
        {
            return UniTask.CompletedTask;
    
        }
        return StartAbilityImplementation(DATA, enemy);
    }


    public abstract UniTask StartAbilityImplementation(AbilityData DATA, IWarrior enemy);
    

}

public struct AbilityData
{
    public readonly AnilityBase bility;
    public readonly IWarrior warrior;
    public readonly EDiceType[] dicee;
    public readonly int value;
    public AbilityData(AnilityBase bility, IWarrior warrioir, EDiceType[] dicee, int value)
    {
        this.warrior = warrioir;
        this.dicee = dicee; 
        this.value = value;
        this.bility = bility;
    }
}


