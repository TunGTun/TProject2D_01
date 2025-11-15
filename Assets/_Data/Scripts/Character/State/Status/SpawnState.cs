using UnityEngine;

public class SpawnState : ICharState<CharBaseState>
{
    public string Name => StateName.SPAWN_STATE;

    public FSMType FSMType => FSMType.Status;

    protected float timer;

    public void OnEnter(CharBaseState context)
    {
        timer = SCharStaticData.SpawnDuration;

        context.CharCtrl.AnimationCtrl.UpdateAnimation();

        //AudioManager.Instance.PlaySFX(ESoundName.Dead);

        context.CharCtrl.CharDamageReceiver.CanTakeDamage = false;
        InputManager.Instance.SetCanControl(false);
    }

    public void OnExit(CharBaseState context)
    {
        context.CharCtrl.CharDamageReceiver.CanTakeDamage = true;
        InputManager.Instance.SetCanControl(true);
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