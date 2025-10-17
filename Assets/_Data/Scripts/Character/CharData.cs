using UnityEngine;

public class CharData : MyMonoBehaviour
{
    [SerializeField] protected float gravityScale = 2.5f;
    public float GravityScale => gravityScale;

    [SerializeField] protected float bufferWindow = 0.2f;
    public float BufferWindow => bufferWindow;

    [Header("Stats")]
    [SerializeField] protected int maxHP = 5;
    public int MaxHP => maxHP;

    [SerializeField] protected int attackDamage = 5;
    public int AttackDamage => attackDamage;

    [Header("Current Stats")]
    [SerializeField] protected int currentHP;
    public int CurrentHP { get => currentHP; set => currentHP = value; }

    [Header("Move")]
    [SerializeField] protected float moveSpeed = 4f;
    public float MoveSpeed => moveSpeed;

    [Header("Jump")]
    [SerializeField] protected float jumpForce = 13f;
    public float JumpForce => jumpForce;
    
    [Header("DoubleJump")]
    [SerializeField] protected float doubleJumpForce = 8f;
    public float DoubleJumpForce => doubleJumpForce;

    [Header("Attack")]

    [SerializeField] protected float attackDuration = 0.5f;
    public float AttackDuration => attackDuration;

    [Header("Dash")]

    [SerializeField] protected float dashDuration = 0.1f;
    public float DashDuration => dashDuration;

    [SerializeField] protected float dashForce = 30f;
    public float DashForce => dashForce;

    [SerializeField] protected float dashCoolDown = 1f;
    public float DashCoolDown => dashCoolDown;

    [Header("Combat")]
    [SerializeField] protected float hurtTime = 0.25f;
    public float HurtTime => hurtTime;

    protected override void Start()
    {
        base.Start();
        this.Init();
    }

    protected virtual void Init()
    {
        this.currentHP = this.maxHP;
    }
}
