using UnityEngine;

public class DashState : ICharState<CharBaseState>
{
    public string Name => StateName.DASH_STATE;

    public FSMType FSMType => FSMType.Skill;

    protected float timer;

    public void OnEnter(CharBaseState context)
    {
        Transform voidRift = VoidRiftSpawner.Instance.CurrentRift;

        if (voidRift != null)
        {
            context.CharCtrl.transform.position = voidRift.position;
            VoidRiftSpawner.Instance.Despawn(voidRift);
            context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.idleSkill);
            context.CharCtrl.CharStateCtrl.ResetSkill();
            return;
        }

        if (!context.CharCtrl.CharStateCtrl.SkillLock.IsUnlocked(ESkill.Dash))
        {
            context.CharCtrl.CharStateCtrl.SkillState.ChangeState(
                context.CharCtrl.CharStateCtrl.SkillState.idleSkill);
            return;
        }
        
        if (!context.CharCtrl.CharStateCtrl.canDash)
        {
            context.CharCtrl.CharStateCtrl.SkillState.ChangeState(
                context.CharCtrl.CharStateCtrl.SkillState.idleSkill);
            return;
        }

        timer = context.CharCtrl.CharData.DashDuration;
        
        context.CharCtrl.CharStateCtrl.canDash = false;

        context.CharCtrl.RigidBody2D.gravityScale = 0;
        
        context.CharCtrl.CharStateCtrl.VelocityHandle.Request(0f, 0f, 100);

        // context.CharCtrl.CharStateCtrl.FlipX();

        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        context.CharCtrl.RigidBody2D.gravityScale = context.CharCtrl.CharData.GravityScale;

        //context.CharCtrl.NextDashTime = Time.time + context.CharCtrl.CharData.DashCooldown;
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            context.CharCtrl.CharStateCtrl.VelocityHandle.Request(0f, 0f, 100);
            context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.idleSkill);
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
        float dir = context.transform.parent.localScale.x;
        float dashSpeed = context.CharCtrl.CharData.DashForce;

        context.CharCtrl.CharStateCtrl.VelocityHandle.RequestX(dir * dashSpeed, 100);
    }
}
