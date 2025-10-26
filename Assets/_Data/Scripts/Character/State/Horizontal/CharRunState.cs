using Unity.VisualScripting;
using UnityEngine;

public class CharRunState : ICharState<CharBaseState>
{
    public string Name => StateName.RUN_STATE;

    public FSMType FSMType => FSMType.Horizontal;

    public void OnEnter(CharBaseState context)
    {
        //Debug.Log("CharMoveXState Enter");
        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        //Debug.Log("CharMoveXState Exit");
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        if (InputManager.Instance.MoveInput == 0)
        {
            context.CharCtrl.CharStateCtrl.HorizontalState.ChangeState(context.CharCtrl.CharStateCtrl.HorizontalState.idleX);
            return;
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
        if (context.CharCtrl.CharStateCtrl.SkillState.StateMachine.CurrentState != context.CharCtrl.CharStateCtrl.SkillState.attack)
            context.CharCtrl.CharStateCtrl.FlipX();

        if (context.CharCtrl.CharStateCtrl.SkillState.StateMachine.CurrentState == context.CharCtrl.CharStateCtrl.SkillState.dash) return;

        float moveInput = InputManager.Instance.MoveInput;
        float moveSpeed = SCharStaticData.MoveSpeed;

        context.CharCtrl.RigidBody2D.linearVelocity = new Vector2(moveInput * moveSpeed, context.CharCtrl.RigidBody2D.linearVelocity.y);
    }
}
