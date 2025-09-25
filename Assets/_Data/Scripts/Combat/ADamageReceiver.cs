using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]

public abstract class ADamageReceiver : MyMonoBehaviour
{
    [Header("ADamageReceiver")]
    [SerializeField] protected CapsuleCollider2D hitBoxCollider;
    public CapsuleCollider2D HitBoxCollider => hitBoxCollider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHitBoxCollider();
    }

    protected virtual void LoadHitBoxCollider()
    {
        if (hitBoxCollider != null) return;
        this.hitBoxCollider = GetComponent<CapsuleCollider2D>();
        Debug.Log(transform.name + ": LoadHitBoxCollider", gameObject);
    }

    public abstract void OnDamageReceived();
}
