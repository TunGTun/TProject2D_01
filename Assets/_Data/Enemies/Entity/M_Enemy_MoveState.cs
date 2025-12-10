using UnityEngine;

public class M_Enemy_MoveState : M_Enemy_GroundedState
{
    public M_Enemy_MoveState(M_Enemy enemy, M_StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (enemy.groundDetected == false || enemy.wallDetected)
            enemy.Flip();
    }


    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.linearVelocity.y);

        if (enemy.groundDetected == false || enemy.wallDetected)
            stateMachine.ChangeState(enemy.idleState);
    }
}
