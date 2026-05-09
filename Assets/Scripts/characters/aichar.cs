using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

public class aichar : basechar
{
    public override async UniTask DewTurn()
    {
        await RollDice();
        

    }
}
