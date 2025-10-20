using UnityEngine;

public class IdleGroundState : ICharState<CharBaseState>
{
    public string Name => "";

    public FSMType FSMType => FSMType.Default;

    public void OnEnter(CharBaseState context)
    {
        context.CharCtrl.RigidBody2D.linearVelocity = new Vector2(context.CharCtrl.RigidBody2D.linearVelocity.x, 0);

        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        //Debug.Log("IdleGroundState Exit");
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        //context.CharCtrl.CharStateCtrl.VerticalState.jump.ResetJumpCount(context);
        if (InputManager.Instance.JumpInputDown && context.CharCtrl.EnvironmentChecker.IsGrounded)
        {
            context.CharCtrl.CharStateCtrl.VerticalState.ChangeState(context.CharCtrl.CharStateCtrl.VerticalState.jump);
        }

        if (context.CharCtrl.RigidBody2D.linearVelocityY < 0)
        {
            context.CharCtrl.CharStateCtrl.VerticalState.ChangeState(context.CharCtrl.CharStateCtrl.VerticalState.fall);
        }

        context.CharCtrl.CharStateCtrl.ResetSkill();
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
    }
}
