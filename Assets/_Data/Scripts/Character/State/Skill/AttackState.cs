using System.Collections;
using UnityEngine;

public class AttackState : ICharState<CharBaseState>
{
    public string Name => StateName.ATTACK_STATE;

    public FSMType FSMType => FSMType.Skill;

    protected float timer;
    protected bool hasChained;

    public void OnEnter(CharBaseState context)
    {
        //Debug.Log("AttackState Enter");

        hasChained = false;

        timer = context.CharCtrl.CharData.AttackDuration;

        //context.CharCtrl.RigidBody2D.gravityScale = 0;

        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        //context.CharCtrl.RigidBody2D.gravityScale = context.CharCtrl.CharData.GravityScale;

        //Debug.Log("AttackState Exit");
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        timer -= Time.deltaTime;

        if (!hasChained && timer <= context.CharCtrl.CharData.BufferWindow)
        {
            if (InputManager.Instance.LeftMouseClick)
            {
                timer += context.CharCtrl.CharData.AttackDuration;
                this.hasChained = true;
            }
        }

        if (timer <= 0f)
        {
            context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.idleSkill);
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
        context.CharCtrl.RigidBody2D.linearVelocity = Vector2.zero;
    }
}
