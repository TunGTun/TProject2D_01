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

        float moveInput = InputManager.Instance.MoveInput;
        float moveSpeed = context.CharCtrl.CharData.MoveSpeed;

        context.CharCtrl.CharStateCtrl.VelocityHandle.RequestX(moveInput * moveSpeed, SVelocityPriority.Horizontal);
    }
}
