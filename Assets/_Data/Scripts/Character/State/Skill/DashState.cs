using UnityEngine;

public class DashState : ICharState<CharBaseState>
{
    public string Name => StateName.DASH_STATE;

    public FSMType FSMType => FSMType.Skill;

    protected float timer;

    public void OnEnter(CharBaseState context)
    {
        //if (Time.time < context.CharCtrl.CharData.DashCoolDown)
        //{
        //    context.CharCtrl.CharStateCtrl.HorizontalState.ChangeState(context.CharCtrl.CharStateCtrl.HorizontalState.idleX);
        //    return;
        //}

        Transform voidRift = VoidRiftSpawner.Instance.currentRift;

        if (voidRift != null)
        {
            context.CharCtrl.transform.position = voidRift.position;
            VoidRiftSpawner.Instance.Despawn(voidRift);
            context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.idleSkill);
            return;
        }

        if (!context.CharCtrl.CharStateCtrl.SkillLock.IsUnlocked(ESkill.Dash))
        {
            context.CharCtrl.CharStateCtrl.SkillState.ChangeState(
                context.CharCtrl.CharStateCtrl.SkillState.idleSkill);
            return;
        }

        timer = context.CharCtrl.CharData.DashDuration;

        context.CharCtrl.RigidBody2D.gravityScale = 0;

        context.CharCtrl.CharStateCtrl.FlipX();

        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        context.CharCtrl.RigidBody2D.linearVelocity = Vector2.zero;

        context.CharCtrl.RigidBody2D.gravityScale = context.CharCtrl.CharData.GravityScale;

        //context.CharCtrl.NextDashTime = Time.time + context.CharCtrl.CharData.DashCooldown;
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.idleSkill);
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
        //float direction = context.transform.parent.localScale.x;
        //if (InputManager.Instance.MoveInput != 0) direction = InputManager.Instance.MoveInput;
        context.CharCtrl.RigidBody2D.linearVelocity
            = new Vector2(context.transform.parent.localScale.x * context.CharCtrl.CharData.DashForce, 0f);
    }
}
