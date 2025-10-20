using UnityEngine;

public class CharIdleXState : ICharState<CharBaseState>
{
    public string Name => StateName.IDLE_STATE;

    public FSMType FSMType => FSMType.Horizontal;

    public void OnEnter(CharBaseState context)
    {
        context.CharCtrl.RigidBody2D.linearVelocity = new Vector2(0f, context.CharCtrl.RigidBody2D.linearVelocity.y);

        context.CharCtrl.AnimationCtrl.UpdateAnimation();

    }

    public void OnExit(CharBaseState context)
    {
        //Debug.Log("CharIdleXState Exit");
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        if (InputManager.Instance.MoveInput != 0)
        {
            context.CharCtrl.CharStateCtrl.HorizontalState.ChangeState(context.CharCtrl.CharStateCtrl.HorizontalState.run);
            return;
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
        if (context.CharCtrl.CharStateCtrl.SkillState.StateMachine.CurrentState == context.CharCtrl.CharStateCtrl.SkillState.dash) return;
        context.CharCtrl.RigidBody2D.linearVelocity = new Vector2(0f, context.CharCtrl.RigidBody2D.linearVelocity.y);
    }
}
