using UnityEngine;

public class BossFastJumpState : IState<BossBaseState>
{
    protected float originalGravity;

    public void OnEnter(BossBaseState boss)
    {
        originalGravity = boss.BossCtrl.Rigidbody2D.gravityScale;
        boss.BossCtrl.Rigidbody2D.gravityScale = BossData.newGravity;

        Vector2 startPos = boss.BossCtrl.transform.position;

        Vector2 leftBottomPos = boss.BossCtrl.leftBottomPos.position;
        Vector2 rightBottomPos = boss.BossCtrl.rightBottomPos.position;

        float targetX = boss.BossCtrl.Target.position.x;

        float leftDistX = Mathf.Abs(leftBottomPos.x - targetX);
        float rightDistX = Mathf.Abs(rightBottomPos.x - targetX);

        Vector2 endPos = (leftDistX > rightDistX) ? leftBottomPos : rightBottomPos;

        endPos = new Vector2(endPos.x, startPos.y);

        Vector2 distance = endPos - startPos;

        float vx = distance.x / BossData.airTimeFast;

        float gravity = Mathf.Abs(Physics2D.gravity.y * boss.BossCtrl.Rigidbody2D.gravityScale);
        float vy = (distance.y + 0.5f * gravity * BossData.airTimeFast * BossData.airTimeFast) / BossData.airTimeFast;

        Vector2 velocity = new Vector2(vx, vy);
        boss.BossCtrl.Rigidbody2D.linearVelocity = velocity;

        Debug.Log("BossFastJumpState Enter");
    }

    public void OnExit(BossBaseState boss)
    {
        boss.BossCtrl.Rigidbody2D.gravityScale = originalGravity;
        Debug.Log("BossFastJumpState Exit");
    }

    public void OnFrameUpdate(BossBaseState boss)
    {

    }

    public void OnPhysicUpdate(BossBaseState boss)
    {
        Debug.Log("BossFastJumpState PhysicUpdate");
    }
}
