using UnityEngine;

public class NormalState : ICharState<CharBaseState>
{
    public string Name => StateName.IDLE_STATE;

    public FSMType FSMType => FSMType.Default;

    public void OnEnter(CharBaseState context)
    {
        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        //Debug.Log("NormalState Exit");
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        if (InputManager.Instance.HealInput)
        {
            if (!context.CharCtrl.CharData.UseMP(SCharStaticData.HealMP))
            {
                Debug.Log("Khong du nang luong hoi mau");
                return;
            }
            context.CharCtrl.CharStateCtrl.StatusState.ChangeState(context.CharCtrl.CharStateCtrl.StatusState.heal);
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
    }
}
