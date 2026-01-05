using UnityEngine;

public class CutSceneState : ICharState<CharBaseState>
{
    public string Name => StateName.CUT_SCENE_STATE;

    public FSMType FSMType => FSMType.Default;

    public void OnEnter(CharBaseState context)
    {
        context.CharCtrl.RigidBody2D.linearVelocity = Vector2.zero;
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
