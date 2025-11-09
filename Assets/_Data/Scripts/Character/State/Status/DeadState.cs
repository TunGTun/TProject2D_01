using System.Collections;
using UnityEngine;

public class DeadState : ICharState<CharBaseState>
{
    public string Name => StateName.DEAD_STATE;

    public FSMType FSMType => FSMType.Status;

    public void OnEnter(CharBaseState context)
    {
        context.CharCtrl.RigidBody2D.linearVelocity = Vector2.zero;
        
        context.CharCtrl.RigidBody2D.gravityScale = 0;

        context.CharCtrl.AnimationCtrl.UpdateAnimation();

        AudioManager.Instance.PlaySFX(ESoundName.Dead);

        context.CharCtrl.CharDamageReceiver.CanTakeDamage = false;
        InputManager.Instance.SetCanControl(false);

        Vector3 spawnPos = context.CharCtrl.transform.position;
        FXSpawner.Instance.Spawn(FXSpawner.Instance.DEAD, spawnPos, Quaternion.identity);
    }

    public void OnExit(CharBaseState context)
    {
        context.CharCtrl.RigidBody2D.gravityScale = SCharStaticData.GravityScale;

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
