using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAbility", menuName = "Ability/AttackAbility")]
public class AttackAbility : AnilityBase
{
    [SerializeField] private string animationID = "Attack1";
    [SerializeField] private AnimationClip animation;
    [SerializeField] private int eventIndex = 1;
    [SerializeField] private int attackThreshhold;
    [SerializeField] private AttackAbility attackCombo;
    [SerializeField] private float attackDist = 1f;
    [SerializeField] private float moveTime = 0.2f;
   

    public override async UniTask StartAbilityImplementation(AbilityData DATA, IWarrior enemy)
    {
        if (DATA.warrior is not basechar player || enemy is not basechar enemyCharacter)
        {
            enemy.TakeDamage(DATA.value);
            return;
        }



        Animator currentAnimator = player.GetComponentInChildren<Animator>();


        if (currentAnimator is null)
        {
            Debug.Log(" errorbruhuh ur pc is broken (The game isn't made properly - Gabe Kotton) ");
            enemy.TakeDamage(DATA.value);
            return;
        }

        Vector2 myPos = player.transform.position;
        Vector2 notMyPos = enemyCharacter.transform.position;

        await MoveTo(player.transform, myPos, notMyPos - (notMyPos - myPos).normalized * attackDist, moveTime);
        await Costco(currentAnimator, DATA.value, enemyCharacter, player);
        await UniTask.Delay(1000);
        await MoveTo(player.transform, notMyPos - (notMyPos - myPos).normalized * attackDist, myPos, moveTime);

    }

    private async UniTask Costco(Animator currentAnimator, int dATA, basechar enemyCharacter, basechar player)
    {
        currentAnimator.SetTrigger(animationID);
        int Duration = (int)(animation.events[eventIndex].time * 1000);
        int totalTIme = (int)(animation.length * 1000);
        await UniTask.Delay(Duration);
        int Damage = dATA;
        if (attackCombo)
        {
            Damage = Mathf.Min(Damage, attackThreshhold);
        }
        enemyCharacter.TakeDamage(Damage);
        await UniTask.Delay(totalTIme - Duration + 300);

        if (attackCombo && Damage >= attackThreshhold)
        {
            await attackCombo.Costco(currentAnimator, dATA - attackThreshhold, enemyCharacter, player);
        }

    }

    private async UniTask MoveTo(Transform playerTransform, Vector2 playerPos, Vector2 enemyPos, float f)
    {
        if (enemyPos == playerPos)
            return;
        float currentTime = 0;
        while (currentTime < f)
        {
            float t = currentTime / f;
            playerTransform.position = Vector3.Lerp(playerPos, enemyPos, t);
            currentTime += Time.deltaTime;
            await UniTask.Yield();
           
        }
        playerTransform.position = Vector3.Lerp(playerPos, enemyPos, 1);
    }





    
}
