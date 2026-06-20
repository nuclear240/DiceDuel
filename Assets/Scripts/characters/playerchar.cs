using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class playerchar : basechar
{
    bool IsTurnRunning = false;
    [SerializeField] Canvas Button;



    [SerializeField] AbilityManager abilityManager;

    public override async UniTask DewTurn() {

        IsTurnRunning = true;
        Button.enabled = true;
        await UniTask.WaitWhile(TurnRunning);
       abilities =  abilityManager.retreaveData(this);
       Debug.Log("Player Turn Complete", gameObject);

}

    public bool TurnRunning()
    {
        return IsTurnRunning;
    }

    public override void Initialise()
    {
        base.Initialise();
        abilityManager.RegenerateAbilities(activeAbilitied);
        abilityManager.GenerateDiceUI(DiceToRoll);
    }

    public void CompleteTurn()
    {
        Button.enabled = false;
        IsTurnRunning = false;

    }

}