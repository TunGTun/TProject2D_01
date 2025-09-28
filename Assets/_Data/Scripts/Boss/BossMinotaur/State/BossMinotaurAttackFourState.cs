using UnityEngine;

public class BossMinotaurAttackFourState : IBossState<BaseBossState>
{
    public string Name => "Attack_4";

    private float attackTimer;

    private bool hasSpawned;

    public void OnEnter(BaseBossState boss)
    {
        attackTimer = 0;

        hasSpawned = false;

        boss.BaseBossCtrl.BossAnimationCtrl.ChangeAnimationState(this.Name);
    }

    public void OnExit(BaseBossState context)
    {

    }

    public void OnFrameUpdate(BaseBossState context)
    {
        attackTimer += Time.deltaTime;

        if (!hasSpawned && attackTimer >= 0.6f)
        {
            hasSpawned = true;
            BossMinotaurSkillSpawner.Instance.Spawn(
                BossMinotaurSkillSpawner.Instance.AttackFourHitBox,
                (context.BaseBossCtrl.BaseBossPointCtrl as BossMinotaurPointCtrl).AttackFourHitBox.transform.position,
                Quaternion.Euler(0, 0, 90)
            );
        }

        if (attackTimer >= SBossMinotaurStaticData.AttackFourTime)
        {
            context.BaseBossCtrl.BaseBossState.ChangeState(context.BaseBossCtrl.BaseBossState.idle);
        }
    }

    public void OnPhysicUpdate(BaseBossState boss)
    {

    }
}
