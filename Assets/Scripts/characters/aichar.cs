using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

public class aichar : basechar
{
    public override async UniTask DewTurn()
    {

        AnilityBase btuh = activeAbilitied[Random.Range(0, activeAbilitied.Length)];
        abilities.Add(new AbilityData (
        
             btuh,
             this,
             DiceToRoll,
            0


        ));
        await UniTask.CompletedTask; 

    }
}
