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
        if (context.CharCtrl.CharStateCtrl.InputBuffer.TryConsume(out string action))
        {
            if (action == StateName.DOUBLE_JUMP_STATE)
            {
                if (context.CharCtrl.EnvironmentChecker.IsGrounded) return;
                context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.doubleJump);
                return;
            }
            if (action == StateName.DASH_STATE)
            {
                context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.dash);
                return;
            }
            if (action == StateName.ATTACK_ONE_STATE)
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
