using UnityEngine;

public class M_Enemy_DeadState : M_EnemyState
{
    private Collider2D col;

    public M_Enemy_DeadState(M_Enemy enemy, M_StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        col = enemy.GetComponent<Collider2D>();
    }

    public override void Enter()
    {
        anim.enabled = false;
        col.enabled = false;

        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);

        stateMachine.SwitchOffStateMachine();

    }
}
