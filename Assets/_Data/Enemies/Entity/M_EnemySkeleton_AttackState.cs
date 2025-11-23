//using UnityEngine;

//public class M_EnemySkeleton_AttackState : M_Enemy_AttackState
//{
//    public M_EnemySkeleton_AttackState(M_Enemy enemy, M_StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
//    {
//    }

//    private float attackTimer;
//    private float attackDuration = 0.2f; 


//    public override void Enter()
//    {
//        base.Enter();
//        (this.enemy as M_Enemy_Skeleton).attackCollider.enabled = true;
//        attackTimer = attackDuration;
//    }

//    public override void Update()
//    {
//        base.Update();

//        attackTimer -= Time.deltaTime;
//        if (attackTimer <= 0f)
//        {
//            (enemy as M_Enemy_Skeleton).attackCollider.enabled = false;
//        }
//    }

//    public override void Exit()
//    {
//        base.Exit();

//        (enemy as M_Enemy_Skeleton).attackCollider.enabled = false;
//    }


//}
