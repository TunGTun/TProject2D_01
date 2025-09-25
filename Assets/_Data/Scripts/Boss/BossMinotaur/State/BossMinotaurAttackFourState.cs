using UnityEngine;

public class BossMinotaurAttackFourState : IBossState<BaseBossState>
{
    public string Name => "Attack_4";

    private float attackTimer;

    public void OnEnter(BaseBossState boss)
    {
        attackTimer = 0;

        boss.BaseBossCtrl.BossAnimationCtrl.ChangeAnimationState(this.Name);
    }

    public void OnExit(BaseBossState context)
    {

    }

    public void OnFrameUpdate(BaseBossState context)
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= SBossMinotaurStaticData.AttackFourTime)
        {
            context.BaseBossCtrl.BaseBossState.ChangeState(context.BaseBossCtrl.BaseBossState.idle);
        }
    }

    public void OnPhysicUpdate(BaseBossState boss)
    {

    }
}
