using UnityEngine;

public class M_Enemy_Wolf : M_Enemy
{
    [SerializeField] protected M_EnemyHealth enemyHealth;
    private Vector3 soulSpawnPos;
    private bool hasSpawnedSoul = false;

    protected override void Awake()
    {
        base.Awake();

        idleState = new M_Enemy_IdleState(this, stateMachine, "idle");
        moveState = new M_Enemy_MoveState(this, stateMachine, "move");
        attackState = new M_Enemy_AttackState(this, stateMachine, "attack");
        battleState = new M_Enemy_BattleState(this, stateMachine, "battle");
        deadState = new M_Enemy_DeadState(this, stateMachine, "dead");

        enemyHealth = GetComponent<M_EnemyHealth>();

        /*
        //Load other components or variables here
        whatIsGround = LayerMask.GetMask("Ground");
        groundCheckDistance = 0.74f;
        wallCheckDistance = 0.5f;
        groundCheck = transform.Find("GroundCheck");
        primaryWallCheck = transform.Find("Enemy_Skeleton");
        secondaryWallCheck = transform.Find("Enemy_Skeleton");

        battleMoveSpeed = 3.0f;
        attackDistance = 2f;
        battleTimeDuration = 5f;
        minRetreatDistance = 1f;
        retreatVelocity = new Vector2();

        idleTime = 2f;
        moveSpeed = 1.4f;
        moveAnimSpeedMultiplier = 1.0f;


        whatIsPlayer = LayerMask.GetMask("Player");
        playerCheck= transform.Find("Enemy_Skeleton");
        playerCheckDistance = 5f;
        */

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        if (enemyHealth.isDead && !hasSpawnedSoul)
        {
            hasSpawnedSoul = true;

            // Spawn  drop loot
            soulSpawnPos = transform.position + Vector3.down * 0.5f;

            Transform soul = FXSpawner.Instance.Spawn(FXSpawner.Instance.SOUL, soulSpawnPos, Quaternion.identity);

            soul.GetComponent<M_SoulReward>().SetReward(minSoul, maxSoul);

            Destroy(gameObject, 5f);
        }
    }

}
