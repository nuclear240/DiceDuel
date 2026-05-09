using Cysharp.Threading.Tasks;
using Given.Manager;
using UnityEngine;

public interface IWarrior
{
    void EndRound();
    bool IsAlive();
    UniTask DewTurn();
    void StartRound();
    void TakeDamage();
    EDiceType[] GetBattleDice();
    void Initialise();
}
