using UnityEngine;

public class DoubleJumpState : ICharState<CharBaseState>
{
    public string Name => StateName.JUMP_STATE;

    public FSMType FSMType => FSMType.Skill;

    private Transform fx;

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

        // context.CharCtrl.RigidBody2D.linearVelocity = new Vector2(context.CharCtrl.RigidBody2D.linearVelocity.x, 0);
        context.CharCtrl.CharStateCtrl.VelocityHandle.RequestY(0, SVelocityPriority.Skill);
        context.CharCtrl.RigidBody2D.AddForce(Vector2.up * context.CharCtrl.CharData.DoubleJumpForce, ForceMode2D.Impulse);

        context.CharCtrl.AnimationCtrl.UpdateAnimation();

        Vector3 spawnPos = context.CharCtrl.AnimationCtrl.transform.position;
        Quaternion spawnRot = Mathf.Approximately(context.CharCtrl.transform.localScale.x, 1) ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
        fx = FXSpawner.Instance.Spawn(FXSpawner.Instance.DOUBLE_JUMP, spawnPos, spawnRot);
    }

    public void OnExit(CharBaseState context)
    {
        if (fx != null)
            FXSpawner.Instance.Despawn(fx);
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        if (context.CharCtrl.RigidBody2D.linearVelocityY <= 0)
        {
            context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.idleSkill);
        }
        
        fx.position = context.CharCtrl.AnimationCtrl.transform.position;
        fx.rotation = Mathf.Approximately(context.CharCtrl.transform.localScale.x, 1) ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
    }

    public void OnPhysicUpdate(CharBaseState context)
    {

    }
}