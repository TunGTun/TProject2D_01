using UnityEngine;

public class M_Enemy : M_Entity
{
    public M_Enemy_IdleState idleState;
    public M_Enemy_MoveState moveState;
    public M_Enemy_AttackState attackState;
    public M_Enemy_BattleState battleState;
    public M_Enemy_DeadState deadState;

    [Header("Battle details")]
    public float battleMoveSpeed = 3;
    public float attackDistance = 2;
    public float battleTimeDuration = 5;
    public float minRetreatDistance = 1;
    public Vector2 retreatVelocity;

    [Header("Movement details")]
    public float idleTime = 2;
    public float moveSpeed = 1.4f;
    [Range(0,2)]
    public float moveAnimSpeedMultiplier = 1;

    [Header("Player detection")]
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected float playerCheckDistance = 10;



    public override void EntityDeath()
    {
        base.EntityDeath();
        stateMachine.ChangeState(deadState);
    }

    public RaycastHit2D PlayerDetected()
    {
        RaycastHit2D hit =
            Physics2D.Raycast(playerCheck.position - new Vector3(3f,0f,0f) , Vector2.right * facingDir, playerCheckDistance, whatIsPlayer | whatIsGround);

        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;

        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * playerCheckDistance), playerCheck.position.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * attackDistance), playerCheck.position.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * minRetreatDistance), playerCheck.position.y));

    }

}
