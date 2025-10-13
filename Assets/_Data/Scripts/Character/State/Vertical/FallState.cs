public class FallState : ICharState<CharBaseState>
{
    public string Name => StateName.FALL_STATE;

    public FSMType FSMType => FSMType.Vertical;

    public void OnEnter(CharBaseState context)
    {
        //Debug.Log("FallState Enter");
        //context.CharCtrl.RigidBody2D.gravityScale = 3;
        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        //Debug.Log("FallState Exit");
        //context.CharCtrl.RigidBody2D.gravityScale = 1;
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        if (context.CharCtrl.EnvironmentChecker.IsGrounded)
            //&& this.charCtrl.RigidBody2D.linearVelocityY == 0
        {
            context.CharCtrl.CharStateCtrl.VerticalState.ChangeState(context.CharCtrl.CharStateCtrl.VerticalState.idleGround);
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
        
    }
}
