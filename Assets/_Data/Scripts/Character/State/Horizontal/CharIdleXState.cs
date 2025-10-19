using UnityEngine;

public class CharIdleXState : ICharState<CharBaseState>
{
    public string Name => StateName.IDLE_STATE;

    public FSMType FSMType => FSMType.Horizontal;

    public void OnEnter(CharBaseState context)
    {
        context.CharCtrl.CharStateCtrl.VelocityHandle.RequestX(0f, SVelocityPriority.Horizontal);
        //Debug.Log("CharIdleXState Enter");

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
        context.CharCtrl.CharStateCtrl.VelocityHandle.RequestX(0f, SVelocityPriority.Horizontal);
    }
}
