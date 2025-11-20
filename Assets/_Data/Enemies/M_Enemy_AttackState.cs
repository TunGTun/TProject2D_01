using UnityEngine;

public class M_Enemy_AttackState : M_EnemyState
{
    public M_Enemy_AttackState(M_Enemy enemy, M_StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }


    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);

    }
}
