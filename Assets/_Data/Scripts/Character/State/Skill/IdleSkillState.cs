using UnityEngine;

public class IdleSkillState : ICharState<CharBaseState>
{
    public string Name => "";

    public FSMType FSMType => FSMType.Default;

    public void OnEnter(CharBaseState context)
    {
        //Debug.Log("IdleSkillState Enter");
        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        //Debug.Log("IdleSkillState Exit");
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        //if (InputManager.Instance.LeftShiftInput || InputManager.Instance.LeftCtrlInput)
        //{
        //    context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.dash);
        //    return;
        //}

        //if (InputManager.Instance.LeftMouseClick)
        //{
        //    context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.attack);
        //    return;
        //}

        if (context.CharCtrl.CharStateCtrl.InputBuffer.TryConsume(out string action))
        {
            if (action == StateName.DASH_STATE)
            {
                context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.dash);
                return;
            }
            if (action == StateName.ATTACK_STATE)
            {
                context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.attack);
                return;
            }
        }

    }

    public void OnPhysicUpdate(CharBaseState context)
    {
    }
}
