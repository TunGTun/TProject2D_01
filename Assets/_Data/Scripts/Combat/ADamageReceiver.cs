using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]

public abstract class ADamageReceiver : MyMonoBehaviour
{
    [Header("ADamageReceiver")]

    [SerializeField] protected BaseBossCtrl baseBossCtrl;
    public BaseBossCtrl BaseBossCtrl => baseBossCtrl;

    [SerializeField] protected CapsuleCollider2D hitBoxCollider;
    public CapsuleCollider2D HitBoxCollider => hitBoxCollider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBaseBossCtrl();
        this.LoadHitBoxCollider();
    }

    protected virtual void LoadBaseBossCtrl()
    {
        if (baseBossCtrl != null) return;
        baseBossCtrl = GetComponentInParent<BaseBossCtrl>();
        Debug.LogWarning(transform.name + ": LoadBaseBossCtrl", gameObject);
    }

    protected virtual void LoadHitBoxCollider()
    {
        if (hitBoxCollider != null) return;
        this.hitBoxCollider = GetComponent<CapsuleCollider2D>();
        Debug.Log(transform.name + ": LoadHitBoxCollider", gameObject);
    }

    public virtual void AddHP(int hp)
    {
        this.baseBossCtrl.BaseBossData.CurrentHealth += hp;
        if (this.baseBossCtrl.BaseBossData.CurrentHealth > SBossMinotaurStaticData.MaxHP)
            this.baseBossCtrl.BaseBossData.CurrentHealth = SBossMinotaurStaticData.MaxHP;
    }

    public virtual void SubHP(int damage)
    {
        this.baseBossCtrl.BaseBossData.CurrentHealth -= damage;
        if (this.baseBossCtrl.BaseBossData.CurrentHealth < 0) 
            this.baseBossCtrl.BaseBossData.CurrentHealth = 0;
    }

    public abstract void OnDamageReceived(int damage);
}
