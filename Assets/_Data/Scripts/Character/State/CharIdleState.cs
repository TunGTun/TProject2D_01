using UnityEngine;

public class CharIdleState : IState<CharState>
{
    public void OnEnter(CharState context)
    {
        context.CharCtrl.RigidBody2D.linearVelocity = new Vector2(0f, context.CharCtrl.RigidBody2D.linearVelocity.y);
        Debug.Log("CharIdleState Enter");
    }

    public void OnExit(CharState context)
    {
        Debug.Log("CharIdleState Exit");
    }

    public void OnFrameUpdate(CharState context)
    {
    }

    public void OnPhysicUpdate(CharState context)
    {
    }
}
