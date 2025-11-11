using UnityEngine;

public class HealState : ICharState<CharBaseState>
{
    public string Name => StateName.HEAL_STATE;

    public FSMType FSMType => FSMType.Status;

    protected float timer;

    public void OnEnter(CharBaseState context)
    {
        timer = SCharStaticData.HealDuration;

        context.CharCtrl.RigidBody2D.linearVelocity = Vector2.zero;

        context.CharCtrl.RigidBody2D.gravityScale = 0;

        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        if (timer > 0) Debug.Log("Bi huy hoi mau");
        context.CharCtrl.RigidBody2D.gravityScale = SCharStaticData.GravityScale;
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            context.CharCtrl.CharData.AddHP(SCharStaticData.HealHP);
            context.CharCtrl.CharStateCtrl.StatusState.ChangeState(context.CharCtrl.CharStateCtrl.StatusState.normal);
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
    }
}
