using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class BossMinotaurAttackOneState : IBossState<BaseBossState>
{
    public string Name => "Attack_1";

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

        if (attackTimer >= SBossMinotaurStaticData.AttackOneTime)
        {
            context.BaseBossCtrl.BaseBossState.ChangeState(context.BaseBossCtrl.BaseBossState.idle);
        }
    }

    public void OnPhysicUpdate(BaseBossState boss)
    {

    }
}
