using UnityEngine;
using UnityEngine.Serialization;

public class BaseBossEnvironmentChecker : MyMonoBehaviour
{
    [Header("BaseBossEnvironmentChecker")]

    [SerializeField] protected BaseBossCtrl baseBossCtrl;
    public BaseBossCtrl BaseBossCtrl => baseBossCtrl;

    [Header("WallCheck")]
    [SerializeField] protected bool isBlocked; //Dap vao tuong
    public bool IsBlocked => isBlocked;

    [SerializeField] protected Collider2D wallCollider;
    public Collider2D WallCollider => wallCollider;

    [SerializeField] protected float wallCheckDistance = 0.2f;
    [SerializeField] protected LayerMask wallLayer;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBaseBossCtrl();
        this.LoadWallLayer();
    }

    protected virtual void LoadBaseBossCtrl()
    {
        if (baseBossCtrl != null) return;
        baseBossCtrl = GetComponentInParent<BaseBossCtrl>();
        Debug.LogWarning(transform.name + ": LoadBaseBossCtrl", gameObject);
    }

    protected virtual void LoadWallLayer()
    {
        if (wallLayer != 0) return;
        wallLayer = LayerMask.GetMask("Wall");
    }

    protected virtual void CheckWall()
    {
        float direction = Mathf.Sign(this.BaseBossCtrl.transform.localScale.x);

        Vector2 origin = (Vector2)this.baseBossCtrl.CapsuleCollider2D.bounds.center
                         + new Vector2(direction * this.baseBossCtrl.CapsuleCollider2D.bounds.extents.x, 0);

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * direction, wallCheckDistance, wallLayer);

        this.isBlocked = hit.collider != null;

        if (hit.collider == null) return;
         
        if (hit.collider == this.wallCollider) return;

        this.wallCollider = hit.collider;
    }
}
