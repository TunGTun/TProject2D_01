using UnityEngine;

public class CharData : MyMonoBehaviour
{

    [SerializeField] protected float gravityScale = 3.5f;
    public float GravityScale => gravityScale;

    [SerializeField] protected float bufferWindow = 0.2f;
    public float BufferWindow => bufferWindow;

    [Header("Move")]

    [SerializeField] protected float moveSpeed = 6f;
    public float MoveSpeed => moveSpeed;

    [Header("Jump")]

    [SerializeField] protected float jumpForce = 12f;
    public float JumpForce => jumpForce;

    [SerializeField] protected int maxJump = 2;
    public float MaxJump => maxJump;

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
}
