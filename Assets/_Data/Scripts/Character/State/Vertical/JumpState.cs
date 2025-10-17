using UnityEngine;

public class JumpState : ICharState<CharBaseState>
{
    public string Name => StateName.JUMP_STATE;

    public FSMType FSMType => FSMType.Vertical;

    public void OnEnter(CharBaseState context)
    {
        context.CharCtrl.RigidBody2D.linearVelocity = new Vector2(context.CharCtrl.RigidBody2D.linearVelocity.x, 0);
        context.CharCtrl.RigidBody2D.AddForce(Vector2.up * context.CharCtrl.CharData.JumpForce, ForceMode2D.Impulse);

        context.CharCtrl.AnimationCtrl.UpdateAnimation();

        Vector3 bottomPos = context.CharCtrl.CharBodyCollider.bounds.center - new Vector3(0, context.CharCtrl.CharBodyCollider.bounds.extents.y, 0);
        FXSpawner.Instance.Spawn(FXSpawner.Instance.JUMP, bottomPos, Quaternion.identity);
    }

    public void OnExit(CharBaseState context)
    {
        
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        if (InputManager.Instance.JumpInputUp)
        {
            if (context.CharCtrl.RigidBody2D.linearVelocityY < 0f) return;
            if (context.CharCtrl.CharStateCtrl.SkillState.StateMachine.CurrentState == context.CharCtrl.CharStateCtrl.SkillState.doubleJump) return;
            context.CharCtrl.RigidBody2D.linearVelocity = new Vector2(context.CharCtrl.RigidBody2D.linearVelocity.x,
                                                                context.CharCtrl.RigidBody2D.linearVelocity.y / 4f);
        }

        if (context.CharCtrl.RigidBody2D.linearVelocityY <= 0f)
        {
            context.CharCtrl.CharStateCtrl.VerticalState.ChangeState(context.CharCtrl.CharStateCtrl.VerticalState.fall);
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {

    }
}
