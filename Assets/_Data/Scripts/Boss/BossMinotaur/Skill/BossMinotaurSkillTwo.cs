using System;
using UnityEngine;

public class BossMinotaurSkillTwo
{
    protected EBossMinotaurState phase = EBossMinotaurState.None;

    protected float bossPlayerDistance;

    protected float moveTimer;

    public void Execute(BaseBossCtrl bossCtrl)
    {
        if (bossCtrl.BaseBossControl.IsExecutingSkill) return;

        bossPlayerDistance = MathF.Abs(bossCtrl.transform.position.x - bossCtrl.BossTarget.Target.transform.position.x);
        //if (bossPlayerDistance > SBossMinotaurStaticData.LimitDistance) return;
        
        bossCtrl.BaseBossControl.IsExecutingSkill = true;
        
        phase = EBossMinotaurState.MoveToTarget;
        moveTimer = 0f;
        bossCtrl.BaseBossState.ChangeState(bossCtrl.BaseBossState.moveToTarget);
    }

    public void Tick(BaseBossCtrl bossCtrl) // ?? update
    {
        if (phase == EBossMinotaurState.None) return;

        bossPlayerDistance = MathF.Abs(bossCtrl.transform.position.x - bossCtrl.BossTarget.Target.transform.position.x);

        switch (phase)
        {
            case EBossMinotaurState.MoveToTarget:

                moveTimer += Time.deltaTime;
                if (moveTimer >= SBossMinotaurStaticData.MoveToTargetMaxTime)
                {
                    Cancel(bossCtrl);
                    return;
                }

                if (bossPlayerDistance > SBossMinotaurStaticData.AttackFourRange) return;
                phase = EBossMinotaurState.AttackFour;
                bossCtrl.BaseBossState.ChangeState((bossCtrl.BaseBossState as BossMinotaurState).attackFour);
                break;

            case EBossMinotaurState.AttackFour:
                if (bossCtrl.BaseBossState.StateMachine.CurrentState != bossCtrl.BaseBossState.idle) return;
                this.Cancel(bossCtrl);
                break;
        }
    }

    private void Cancel(BaseBossCtrl bossCtrl)
    {
        phase = EBossMinotaurState.None;
        bossCtrl.BaseBossControl.IsExecutingSkill = false;
    }

    public bool IsRunning()
    {
        return phase != EBossMinotaurState.None;
    }
}
