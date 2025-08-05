using UnityEngine;

public class BossIdleState : IState<BossBaseState>
{
    public void OnEnter(BossBaseState boss)
    {
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
        boss.BossCtrl.Rigidbody2D.linearVelocity = Vector3.zero;
        Debug.Log("BossIdleState PhysicUpdate");
    }
}
