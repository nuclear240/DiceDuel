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
        leftCharacter.target = rightCharacter;
        rightCharacter.target = leftCharacter;
        
        leftCharacter.Initialise();
        rightCharacter.Initialise();

        Debug.Log("Game Start");

        
        while(BattleIsRunnning())
        {
            Debug.Log("Round start");

            leftCharacter.StartRound();
            rightCharacter.StartRound();
            
            Debug.Log("Do Turn");
            
            await UniTask.WhenAll(leftCharacter.DewTurn(), rightCharacter.DewTurn());
            
            Debug.Log("Roll Dice");
            
            await UniTask.WhenAll(leftCharacter.RollDice(), rightCharacter.RollDice());
        
            Debug.Log("Use Bility");
            
            //To fix abilities, we can use a 'QUEUE' system and instead of doing UseAbilities, we get the next ability.
            //If the ability is null we ignore it other goes first. if both null, then proceed.
            //If the ability is higher priority we go first
            //else enemy goes first
            //If the ability is same, if we had the lower role, we go first.
            //else enemy goes first.
            
            //****After any player goes, check if the battle had ended.****//
            
            await UniTask.WhenAll(leftCharacter.UseAbilities(), rightCharacter.UseAbilities());
            
            Debug.Log("End Turn");
            
            leftCharacter.EndRound();
            rightCharacter.EndRound();
            
            
            Debug.Log("Round Complete");
            
        }
        
        Debug.Log($"Battle has ended. Left is alive? {leftCharacter.IsAlive()}, Right is alive? {rightCharacter.IsAlive()}");

    }

    private bool BattleIsRunnning()
    {
        return leftCharacter.IsAlive() && rightCharacter.IsAlive();
    }
}
