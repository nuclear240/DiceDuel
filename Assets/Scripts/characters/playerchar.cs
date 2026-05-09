using Cysharp.Threading.Tasks;
using UnityEngine;

public class playerchar : basechar
{
    [SerializeField] AbilityManager abilityManager;

    public override async UniTask DewTurn() {

        
        await RollDice();
    
}

    public override void Initialise()
    {
        base.Initialise();
        abilityManager.RegenerateAbilities(activeAbilitied);
        abilityManager.GenerateDiceUI(DiceToRoll);
    }



}