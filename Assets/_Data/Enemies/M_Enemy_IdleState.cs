using UnityEngine;

public class M_Enemy_IdleState : M_Enemy_GroundedState
{
    public M_Enemy_IdleState(M_Enemy enemy, M_StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);

    }
}
