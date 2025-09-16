using UnityEngine;

public class BossIdleState : IState<BossBaseState>
{
    public string Name => "";

    public void OnEnter(BossBaseState boss)
    {
        boss.BossCtrl.Rigidbody2D.linearVelocity = new Vector2(0f, boss.BossCtrl.Rigidbody2D.linearVelocity.y);
        Debug.Log("BossIdleState Enter");
    }

    public void OnExit(BossBaseState boss)
    {
        Debug.Log("BossIdleState Exit");
    }

    public void OnFrameUpdate(BossBaseState boss)
    {
        
    }

    public void OnPhysicUpdate(BossBaseState boss)
    {
        Debug.Log("BossIdleState PhysicUpdate");
    }
}
