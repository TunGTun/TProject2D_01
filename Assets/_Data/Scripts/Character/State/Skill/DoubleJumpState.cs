using UnityEngine;

public class DoubleJumpState : ICharState<CharBaseState>
{
    public string Name => StateName.JUMP_STATE;

    public FSMType FSMType => FSMType.Skill;

    //protected int jumpCount = 0;

    public void OnEnter(CharBaseState context)
    {
        if (!context.CharCtrl.CharStateCtrl.SkillLock.IsUnlocked(ESkill.DoubleJump))
        {
            context.CharCtrl.CharStateCtrl.SkillState.ChangeState(
                context.CharCtrl.CharStateCtrl.SkillState.idleSkill);
            return;
        }
        
        if (!context.CharCtrl.CharStateCtrl.canDoubleJump)
        {
            context.CharCtrl.CharStateCtrl.SkillState.ChangeState(
                context.CharCtrl.CharStateCtrl.SkillState.idleSkill);
            return;
        }
        
        context.CharCtrl.CharStateCtrl.canDoubleJump = false;

        context.CharCtrl.RigidBody2D.linearVelocity = new Vector2(context.CharCtrl.RigidBody2D.linearVelocity.x, 0);
        context.CharCtrl.RigidBody2D.AddForce(Vector2.up * context.CharCtrl.CharData.DoubleJumpForce, ForceMode2D.Impulse);

        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        //Debug.Log("JumpState Exit");
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        if (context.CharCtrl.RigidBody2D.linearVelocityY <= 0)
        {
            context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.idleSkill);
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {

    }
}