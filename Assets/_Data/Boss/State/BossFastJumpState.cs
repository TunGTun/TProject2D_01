using UnityEngine;

public class BossFastJumpState : IState<BossBaseState>
{
    public void OnEnter(BossBaseState boss)
    {
        Vector2 startPos = boss.BossCtrl.Rigidbody2D.position;
        Vector2 endPos = new Vector2(boss.BossCtrl.Target.position.x, startPos.y);
        Vector2 distance = endPos - startPos;

        float vx = distance.x / BossData.airTimeFast;

        float gravity = Mathf.Abs(Physics2D.gravity.y * boss.BossCtrl.Rigidbody2D.gravityScale);
        float vy = (distance.y + 0.5f * gravity * BossData.airTimeFast * BossData.airTimeFast) / BossData.airTimeFast;

        Vector2 velocity = new Vector2(vx, vy);
        boss.BossCtrl.Rigidbody2D.linearVelocity = velocity;

        Debug.Log("BossHeavyJumpState Enter");
    }

    public void OnExit(BossBaseState boss)
    {
        Debug.Log("BossHeavyJumpState Exit");
    }

    public void OnFrameUpdate(BossBaseState boss)
    {

    }

    public void OnPhysicUpdate(BossBaseState boss)
    {
        Debug.Log("BossHeavyJumpState PhysicUpdate");
    }
}
