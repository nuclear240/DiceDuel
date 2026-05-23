using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "ConquestAbility", menuName = "Ability/ConquestAbility")]
public class ConquestAbility : AnilityBase
{
    [SerializeField] private AudioClip worm;
    public override async UniTask StartAbilityImplementation(AbilityData DATA, IWarrior enemy)
    {
        AudioSource.PlayClipAtPoint(worm, Vector3.zero);
        await UniTask.CompletedTask;
    }
}
