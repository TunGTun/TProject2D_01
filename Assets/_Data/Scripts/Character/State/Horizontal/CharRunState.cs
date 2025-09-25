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
        if ((context.CharCtrl.CharStateCtrl.SkillState.StateMachine.CurrentState as ICharState<CharBaseState>).FSMType != FSMType.Default) return;

        if (InputManager.Instance.MoveInput == 0)
        {
            context.CharCtrl.CharStateCtrl.HorizontalState.ChangeState(context.CharCtrl.CharStateCtrl.HorizontalState.idleX);
            return;
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {

        this.FlipX(context);

        context.CharCtrl.RigidBody2D.linearVelocity 
            = new Vector2(InputManager.Instance.MoveInput * context.CharCtrl.CharData.MoveSpeed, 
            context.CharCtrl.RigidBody2D.linearVelocity.y);
    }

    protected virtual void FlipX(CharBaseState context)
    {
        if ((context.CharCtrl.CharStateCtrl.SkillState.StateMachine.CurrentState as ICharState<CharBaseState>).FSMType != FSMType.Default) return;
        context.CharCtrl.CharStateCtrl.FlipX();
    }
}
