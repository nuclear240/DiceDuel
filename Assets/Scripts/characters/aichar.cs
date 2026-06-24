using Cysharp.Threading.Tasks;
using Given.Manager;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class aichar : basechar
{
     [field: SerializeField] protected override EDiceType[] DiceToRoll { get; set; }
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

        var assigned = AssingDice();

        string counts = "";
        foreach (var a in assigned)
            counts += a.dicee.Length + ",";
        Debug.Log($"AI assigned {assigned.Length} abilities, dice counts: {counts}", gameObject);

        abilities.AddRange(assigned);
        Debug.Log("AI Turn Complete", gameObject);

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
            int remaining = DiceToRoll.Length - Index;
            bool isLast = j == activeAbilitied.Length - 1;
            int DiceRoll = isLast ? remaining : UnityEngine.Random.Range(0, remaining + 1);
            EDiceType[] dicee = new EDiceType[DiceRoll];
            for (int p = Index; p < Index + DiceRoll; p++)
            {
                dicee[p - Index] = DiceToRoll[p];
            }
            Index += DiceRoll;
            data[j] = new AbilityData(item, target, dicee, 0);
        }
        return data;
    }

    private AbilityData[] ChooseOffensiveAbility()
    {
        throw new System.NotImplementedException();
    }
}
