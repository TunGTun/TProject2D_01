using UnityEngine;

public class M_Enemy_GroundedState : M_EnemyState
{
    public M_Enemy_GroundedState(M_Enemy enemy, M_StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDetected() == true)
            stateMachine.ChangeState(enemy.battleState);
    }

}
