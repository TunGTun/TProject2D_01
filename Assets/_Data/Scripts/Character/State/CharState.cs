using UnityEngine;

public class CharState : BaseState<CharState>
{
    [Header("StateMachine")]
    [SerializeField] protected CharCtrl charCtrl;
    public CharCtrl CharCtrl => charCtrl;

    //public BossIdleState idle;
    //public BossMoveToTargetState move;
    //public BossAttackState attack;

    [Header("State")]
    [SerializeField] protected bool isGrounded;
    public bool IsGrounded => isGrounded;

    [SerializeField] protected bool isDead = false;
    public bool IsDead { get=> isDead; set => isDead = value;  }

    [Header("CheckGrounded")]
    protected int groundMask;
    protected Vector2 boxSize;

    protected override void CreateState()
    {

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharCtrl();
    }

    protected virtual void LoadCharCtrl()
    {
        if (charCtrl != null) return;
        charCtrl = GetComponentInParent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.LoadCheckGroundBox();
    }

    private void Update()
    {
        this.CheckGrounded();
    }

    protected virtual void LoadCheckGroundBox()
    {
        groundMask = LayerMask.GetMask("Ground", "ThroughPlatform");
        boxSize = new Vector2(charCtrl.CharBodyBoxCollider2D.bounds.size.x - 0.02f, 0.05f);
    }

    protected virtual void CheckGrounded()
    {
        Vector2 boxCenter = new Vector2(charCtrl.CharBodyBoxCollider2D.bounds.center.x, charCtrl.CharBodyBoxCollider2D.bounds.min.y - 0.01f);
        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundMask);
        if (hit != null) this.isGrounded = true;
        else this.isGrounded = false;
    }

    // Vẽ trên editer không ảnh hưởng logic game
    private void OnDrawGizmos()
    {
        Vector2 boxSize = new Vector2(
            charCtrl.CharBodyBoxCollider2D.bounds.size.x - 0.02f,
            0.05f
        );

        Vector2 boxCenter = new Vector2(
            charCtrl.CharBodyBoxCollider2D.bounds.center.x,
            charCtrl.CharBodyBoxCollider2D.bounds.min.y - 0.01f
        );

        Gizmos.color = isGrounded ? Color.green : Color.red;

        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}
