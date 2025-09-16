using UnityEngine;

public class BossHeavyJumpState : IState<BossBaseState>
{
    public string Name => throw new System.NotImplementedException();

    public void OnEnter(BossBaseState boss)
    {
        Vector2 startPos = boss.BossCtrl.transform.position;
        Vector2 endPos = new Vector2(boss.BossCtrl.Target.position.x, startPos.y);
        Vector2 distance = endPos - startPos;

        // Tính tốc độ theo trục x
        float vx = distance.x / BossData.airTimeHeavy;

        // Tính tốc độ theo trục y với công thức vật lý rơi tự do
        float gravity = Mathf.Abs(Physics2D.gravity.y * boss.BossCtrl.Rigidbody2D.gravityScale);
        float vy = (distance.y + 0.5f * gravity * BossData.airTimeHeavy * BossData.airTimeHeavy) / BossData.airTimeHeavy;

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
