using Cysharp.Threading.Tasks;
using Given.Manager;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class aichar : basechar
{
    public enum EAIType
    {
        Random,
        Offensive
    }
    [SerializeField] private EAIType aIType;


    public override async UniTask DewTurn()
    {
        if (target is playerchar playerchar)
        {
            await UniTask.WaitWhile(playerchar.TurnRunning);
        }

        abilities.AddRange(AssingDice());

        

       

        await UniTask.CompletedTask; 

    }

    AbilityData[] AssingDice()
    {
        switch (aIType)
        {
            case EAIType.Random:
                return ChooseAbilityRandomly();
                
                case EAIType.Offensive:

                return ChooseOffensiveAbility();

                
        }
        return Array.Empty<AbilityData>();
    }

    private AbilityData[] ChooseAbilityRandomly()
    {
        activeAbilitied.Shuffle();
        DiceToRoll.Shuffle();
        int Index = 0;
        AbilityData[] data = new AbilityData[activeAbilitied.Length];
        for (int j = 0; j < activeAbilitied.Length; j++)
        {
            AnilityBase item = activeAbilitied[j];
            int DiceRoll = UnityEngine.Random.Range(0, DiceToRoll.Length-Index);
            EDiceType[] dicee = new EDiceType[DiceRoll];
            for (int p = Index; p < Index+DiceRoll;  p++)
            {
                dicee[p - Index] = DiceToRoll[p];
            }
            data[j] = new AbilityData(item,target, dicee, 0);
        }
        return data;
    }

    private AbilityData[] ChooseOffensiveAbility()
    {
        throw new System.NotImplementedException();
    }
}
