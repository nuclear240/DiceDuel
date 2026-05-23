using Cysharp.Threading.Tasks;
using Given.Manager;
using UnityEngine;

public interface IWarrior
{
    IWarrior target { get; set; }
    int Shield { get; set; }

    void EndRound();
    bool IsAlive();
    UniTask DewTurn();
    UniTask RollDice();
    void StartRound();
    void TakeDamage(int value);
    EDiceType[] GetBattleDice();
    void Initialise();
    int Heal(int value);
}
