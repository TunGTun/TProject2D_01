using UnityEngine;

public class BossMinotaurTauntState : IBossState<BaseBossState>
{
    public string Name => "Taunt";

    private float tauntTimer;

    public void OnEnter(BaseBossState boss)
    {
        tauntTimer = 0;

        boss.BaseBossCtrl.BossAnimationCtrl.ChangeAnimationState(this.Name);
    }

    public void OnExit(BaseBossState context)
    {

    }

    public void OnFrameUpdate(BaseBossState context)
    {
        tauntTimer += Time.deltaTime;

        if (tauntTimer >= SBossMinotaurStaticData.TauntTime)
        {
            context.BaseBossCtrl.BaseBossState.ChangeState(context.BaseBossCtrl.BaseBossState.idle);
        }
    }

    public void OnPhysicUpdate(BaseBossState boss)
    {

    }
}
