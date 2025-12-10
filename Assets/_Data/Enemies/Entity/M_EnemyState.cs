using UnityEngine;

public class M_EnemyState : M_EntityState
{
    protected M_Enemy enemy;

    public M_EnemyState(M_Enemy enemy, M_StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;

        rb = enemy.rb;
        anim = enemy.anim;
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();

        float battleAnimSpeedMultiplier = enemy.battleMoveSpeed / enemy.moveSpeed;

        anim.SetFloat("battleAnimSpeedMultiplier", battleAnimSpeedMultiplier);
        anim.SetFloat("moveAnimSpeedMultiplier", enemy.moveAnimSpeedMultiplier);
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
    }
}
