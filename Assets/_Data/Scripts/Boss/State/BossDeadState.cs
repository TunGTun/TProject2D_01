using UnityEngine;

public class BossDeadState : IBossState<BaseBossState>
{
    public string Name => "Dead";

    public void OnEnter(BaseBossState boss)
    {

        boss.BaseBossCtrl.BossAnimationCtrl.ChangeAnimationState(this.Name);
    }

    public void OnExit(BaseBossState context)
    {

    }

    public void OnFrameUpdate(BaseBossState context)
    {

    }

    public void OnPhysicUpdate(BaseBossState boss)
    {

    }
}
