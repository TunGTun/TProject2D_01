using UnityEngine;

public class BossMinotaurEnvironmentChecker : BaseBossEnvironmentChecker
{
    [Header("CeilingCheck")]
    [SerializeField] protected Collider2D ceilingCol;
    public Collider2D CeilingCol => ceilingCol;

    [SerializeField] protected LayerMask ceilingLayer;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCeilingLayer();
    }

    protected virtual void LoadCeilingLayer()
    {
        if (ceilingLayer != 0) return;
        ceilingLayer = LayerMask.GetMask("Ceiling");
    }

    private void Update()
    {
        this.CheckWall();
        this.CheckCeiling();
    }

    protected virtual void CheckCeiling()
    {
        if (!this.isBlocked) return;

        Bounds bounds = this.baseBossCtrl.CapsuleCollider2D.bounds;

        Vector2 origin = new Vector2(bounds.center.x, bounds.max.y);

        // float rayLength = this.wallBoxCollider.size.y * this.wallBoxCollider.transform.localScale.y;

        // RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, rayLength, ceilingLayer);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, 100f, ceilingLayer);

        if (hit.collider == null) return;

        if (hit.collider == this.ceilingCol) return;

        this.ceilingCol = hit.collider;
    }

    private void OnDrawGizmosSelected()
    {
        float direction = Mathf.Sign(this.BaseBossCtrl.transform.localScale.x);
        Vector2 origin = (Vector2)this.baseBossCtrl.CapsuleCollider2D.bounds.center
                         + new Vector2(direction * this.baseBossCtrl.CapsuleCollider2D.bounds.extents.x, 0);

        Gizmos.color = isBlocked ? Color.red : Color.green;
        Gizmos.DrawLine(origin, origin + Vector2.right * direction * wallCheckDistance);

        if (wallCollider == null) return;

        if (this.baseBossCtrl == null || this.baseBossCtrl.CapsuleCollider2D == null) return;

        Bounds bounds = this.baseBossCtrl.CapsuleCollider2D.bounds;

        Vector2 origin1 = new Vector2(bounds.center.x, bounds.max.y);

        // float rayLength = this.wallCollider.size.y * this.wallCollider.transform.localScale.y;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origin1, origin1 + Vector2.up * 50f);
    }
}
