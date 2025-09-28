using UnityEngine;

public class BossMinotaurAttackOneState : IBossState<BaseBossState>
{
    public string Name => "Attack_1";

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
                BossMinotaurSkillSpawner.Instance.AttackOneHitBox,
                (context.BaseBossCtrl.BaseBossPointCtrl as BossMinotaurPointCtrl).AttackOneHitBox.transform.position,
                Quaternion.identity
            );
        }

        if (attackTimer >= SBossMinotaurStaticData.AttackOneTime)
        {
            context.BaseBossCtrl.BaseBossState.ChangeState(context.BaseBossCtrl.BaseBossState.idle);
        }
    }

    public void OnPhysicUpdate(BaseBossState boss)
    {

    }
}
