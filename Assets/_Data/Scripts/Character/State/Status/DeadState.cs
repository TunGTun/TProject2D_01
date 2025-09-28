using UnityEngine;

public class DeadState : ICharState<CharBaseState>
{
    public string Name => StateName.DEAD_STATE;

    public FSMType FSMType => FSMType.Status;

    public void OnEnter(CharBaseState context)
    {
        InputManager.Instance.SetCanControl(false);

        context.CharCtrl.RigidBody2D.gravityScale = 0;

        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        InputManager.Instance.SetCanControl(true);

        context.CharCtrl.RigidBody2D.gravityScale = context.CharCtrl.CharData.GravityScale;
    }

    public void OnFrameUpdate(CharBaseState context)
    {
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
    }
}
