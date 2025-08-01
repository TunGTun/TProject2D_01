using UnityEngine;

public class CharState : MyMonoBehaviour
{
    [Header("CharState")]
    [SerializeField] protected bool isGrounded;
    public bool IsGrounded => isGrounded;
    [SerializeField] protected bool isDead = false;
    public bool IsDead { get=> isDead; set => isDead = value;  }

    [SerializeField] protected CharCtrl _charCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharCtrl();
    }

    protected virtual void LoadCharCtrl()
    {
        if (_charCtrl != null) return;
        _charCtrl = GetComponentInParent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    private void Update()
    {
        this.CheckGrounded();
    }

    public virtual void CheckGrounded()
    {
        Vector2 boxSize = new Vector2(_charCtrl.BoxCollider2D.bounds.size.x - 0.02f, 0.05f);
        Vector2 boxCenter = new Vector2(_charCtrl.BoxCollider2D.bounds.center.x, _charCtrl.BoxCollider2D.bounds.min.y - 0.04f);

        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, LayerMask.GetMask("Ground"));

        if (hit != null)
        {
            float angle = Vector2.Angle(hit.transform.up, Vector2.up);
            this.isGrounded = angle < 5f;
        }
        else
        {
            this.isGrounded = false;
        }
    }

}
