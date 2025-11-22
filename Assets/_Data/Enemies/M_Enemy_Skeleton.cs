using UnityEngine;

public class M_Enemy_Skeleton : M_Enemy
{

    protected override void Awake()
    {
        base.Awake();

        idleState = new M_Enemy_IdleState(this, stateMachine, "idle");
        moveState = new M_Enemy_MoveState(this, stateMachine, "move");
        attackState = new M_Enemy_AttackState(this, stateMachine, "attack");
        battleState = new M_Enemy_BattleState(this, stateMachine, "battle");

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
}
