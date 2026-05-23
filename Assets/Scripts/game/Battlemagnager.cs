using Cysharp.Threading.Tasks;
using Given.Manager;
using JetBrains.Annotations;
using System;
using UnityEngine;

public class Battlemagnager 
{
    IWarrior leftCharacter;
    IWarrior rightCharacter;
    
    public Battlemagnager(IWarrior leftCharacter, IWarrior rightCharacter) 
    { 
    this.leftCharacter = leftCharacter;
    this.rightCharacter = rightCharacter;
    }
    public void BeginBattle()
    {
        PlayBattle();
        Debug.Log(mathutility.factorial(6));
    }
    public void EndBattle()
    {

    }
    private async UniTaskVoid PlayBattle()
    {
        leftCharacter.Initialise();
        rightCharacter.Initialise();
        leftCharacter.target = rightCharacter;
        rightCharacter.target = leftCharacter;

        while(BattleIsRunnning())
        {
            leftCharacter.StartRound();
            rightCharacter.StartRound();
            await UniTask.WhenAll(leftCharacter.DewTurn(), rightCharacter.DewTurn());
            await UniTask.WhenAll(leftCharacter.RollDice(), rightCharacter.RollDice());

            leftCharacter.EndRound();
            rightCharacter.EndRound();
            await UniTask.Delay(5000);
        }
    }

    private bool BattleIsRunnning()
    {
        return leftCharacter.IsAlive() && rightCharacter.IsAlive();
    }
}
