using UnityEngine;

public class HurtState : ICharState<CharBaseState>
{
    public string Name => StateName.HURT_STATE;

    public FSMType FSMType => FSMType.Status;

    protected float timer;

    public void OnEnter(CharBaseState context)
    {
        //InputManager.Instance.SetCanControl(false);

        timer = SCharStaticData.HurtTime;

        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        //InputManager.Instance.SetCanControl(true);
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            context.CharCtrl.CharStateCtrl.StatusState.ChangeState(context.CharCtrl.CharStateCtrl.StatusState.normal);
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
    }
}
