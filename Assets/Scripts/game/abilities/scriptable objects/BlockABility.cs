using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockABility", menuName = "Ability/BlockABility")]
public class BlockABility : AnilityBase
{
    [SerializeField] private ParticleSystem controlableparticlesystem;



    public override UniTask StartAbilityImplementation(AbilityData DATA, IWarrior enemy)
    {
        DATA.warrior.Shield+=(DATA.value);

        if (DATA.warrior is basechar player)
        {
            ParticleSystem PS = Instantiate(controlableparticlesystem, player.transform.position, Quaternion.identity);
            Destroy(PS.gameObject, PS.main.duration + PS.main.startLifetime.constantMax);
            return UniTask.Delay((int)(PS.main.duration * 1000));

        }
        return UniTask.CompletedTask;
    }
}
