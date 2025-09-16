using UnityEngine;

public class JumpState : ICharState<CharBaseState>
{
    public string Name => StateName.JUMP_STATE;

    public FSMType FSMType => FSMType.Vertical;

    //protected int jumpCount = 0;

    public void OnEnter(CharBaseState context)
    {
        //Debug.Log("JumpState Enter");
        //this.jumpCount++;
        //if (this.jumpCount > context.CharCtrl.CharData.MaxJump) return;

        context.CharCtrl.RigidBody2D.linearVelocity = new Vector2(context.CharCtrl.RigidBody2D.linearVelocity.x, 0);
        context.CharCtrl.RigidBody2D.AddForce(Vector2.up * context.CharCtrl.CharData.JumpForce, ForceMode2D.Impulse);

        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        //Debug.Log("JumpState Exit");
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        if (context.CharCtrl.RigidBody2D.linearVelocityY <= 0)
        {
            context.CharCtrl.CharStateCtrl.VerticalState.ChangeState(context.CharCtrl.CharStateCtrl.VerticalState.idleGround);
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {

    }

    //public virtual void ResetJumpCount(CharBaseState context)
    //{
    //    if (context.CharCtrl.CharStateCtrl.VerticalState.StateMachine.CurrentState != context.CharCtrl.CharStateCtrl.VerticalState.idleGround) return;
    //    jumpCount = 0;
    //}
}
