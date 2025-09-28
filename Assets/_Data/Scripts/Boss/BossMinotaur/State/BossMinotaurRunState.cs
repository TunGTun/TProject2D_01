using UnityEngine;

public class BossMinotaurRunState : IBossState<BaseBossState>
{
    public string Name => "Run";

    public void OnEnter(BaseBossState boss)
    {

        boss.BaseBossCtrl.BossAnimationCtrl.ChangeAnimationState(this.Name);
    }

    public void OnExit(BaseBossState context)
    {
        context.BaseBossCtrl.BossRigidbody2D.linearVelocity = Vector2.zero;
    }

    public void OnFrameUpdate(BaseBossState context)
    {
        if (context.BaseBossCtrl.BaseBossEnvironmentChecker.IsBlocked)
            context.BaseBossCtrl.BaseBossState.ChangeState(context.BaseBossCtrl.BaseBossState.idle);
    }

    public void OnPhysicUpdate(BaseBossState boss)
    {
        Vector3 direction = new Vector3(boss.BaseBossCtrl.transform.localScale.x, 0f, 0f).normalized;

        boss.BaseBossCtrl.BossRigidbody2D.linearVelocity = direction * SBossMinotaurStaticData.MoveSpeed * 2;
    }
}
