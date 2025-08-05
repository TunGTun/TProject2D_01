using UnityEngine;

public class CharState : MyMonoBehaviour
{
    [Header("CharState")]
    [SerializeField] protected bool isGrounded;
    public bool IsGrounded => isGrounded;
    [SerializeField] protected bool isDead = false;
    public bool IsDead { get=> isDead; set => isDead = value;  }

    [SerializeField] protected CharCtrl charCtrl;

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

    private void Update()
    {
        this.CheckGrounded();
    }

    public virtual void CheckGrounded()
    {
        isGrounded= Physics2D.OverlapAreaAll(charCtrl.CharGroundCollider.CharGroundBoxCollider2D.bounds.min, charCtrl.CharGroundCollider.CharGroundBoxCollider2D.bounds.max, LayerMask.GetMask("Ground")).Length>0;


    }

}
