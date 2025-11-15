using UnityEngine;

public class BossCutSceneState : IBossState<BaseBossState>
{
    public string Name => "CutScene";

    public void OnEnter(BaseBossState boss)
    {
        boss.BaseBossCtrl.BaseBossControl.gameObject.SetActive(false);

        boss.BaseBossCtrl.BossRigidbody2D.linearVelocity = new Vector2(0f, boss.BaseBossCtrl.BossRigidbody2D.linearVelocity.y);

        boss.BaseBossCtrl.BossAnimationCtrl.ChangeAnimationState(this.Name);
    }

    public void OnExit(BaseBossState boss)
    {
        boss.BaseBossCtrl.BaseBossControl.gameObject.SetActive(true);
    }

    public void OnFrameUpdate(BaseBossState boss)
    {

    }

    public void OnPhysicUpdate(BaseBossState boss)
    {
    }
}