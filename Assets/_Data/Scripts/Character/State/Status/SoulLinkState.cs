using UnityEngine;

public class SoulLinkState : ICharState<CharBaseState>
{
    public string Name => StateName.SOUL_LINK_STATE;

    public FSMType FSMType => FSMType.Status;

    public void OnEnter(CharBaseState context)
    {
        context.CharCtrl.RigidBody2D.linearVelocity = Vector2.zero;

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
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
    }
}