using UnityEngine;

public class EnvironmentChecker : BaseChar
{
    [Header("GroundCheck")]

    [SerializeField] protected bool isGrounded;
    public bool IsGrounded => isGrounded;

    [SerializeField] protected Transform groundCheckPoint;
    [SerializeField] protected Vector2 groundCheckSize;
    [SerializeField] protected LayerMask groundLayer;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGroundCheck();
    }

    protected virtual void LoadGroundCheck()
    {
        if (groundCheckPoint != null) return;
        groundCheckPoint = this.transform.Find("GroundCheckPoint");
        groundCheckSize = new Vector2(this.charCtrl.CharBodyCollider.size.x, 0.1f);
        groundLayer = LayerMask.GetMask("Ground");
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    private void Update()
    {
        this.CheckGround();
    }

    protected virtual void CheckGround()
    {
        Collider2D hit = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
        this.isGrounded = hit != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
    }
}
