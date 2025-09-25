using UnityEngine;

public class BossMinotaurTauntState : IBossState<BaseBossState>
{
    public string Name => "Attack_1";

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
