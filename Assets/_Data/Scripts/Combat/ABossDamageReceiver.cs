using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]

public abstract class ABossDamageReceiver : ADamageReceiver
{
    [Header("ABossDamageReceiver")]

    [SerializeField] protected BaseBossCtrl baseBossCtrl;
    public BaseBossCtrl BaseBossCtrl => baseBossCtrl;

    [SerializeField] protected CapsuleCollider2D hitBoxCollider;
    public CapsuleCollider2D HitBoxCollider => hitBoxCollider;

    [Header("Dead")]
    [SerializeField] protected bool isDead;

    //Dead observer ===============================================================
    private List<IBossDeathListener> listeners = new List<IBossDeathListener>();

    public void RegisterListener(IBossDeathListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(IBossDeathListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
    //==============================================================================

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

    public virtual void AddHp(int hp)
    {
        this.baseBossCtrl.BaseBossData.CurrentHealth += hp;
        if (this.baseBossCtrl.BaseBossData.CurrentHealth > SBossMinotaurStaticData.MaxHP)
            this.baseBossCtrl.BaseBossData.CurrentHealth = SBossMinotaurStaticData.MaxHP;
    }

    public virtual void SubHp(int damage)
    {
        this.baseBossCtrl.BaseBossData.CurrentHealth -= damage;
        if (this.baseBossCtrl.BaseBossData.CurrentHealth < 0) 
            this.baseBossCtrl.BaseBossData.CurrentHealth = 0;
    }

    protected virtual bool CheckIsDead()
    {
        if (this.baseBossCtrl.BaseBossData.CurrentHealth <= 0)
            this.isDead = true;
        else
            this.isDead = false;
        return this.isDead;
    }

    protected virtual void OnDead()
    {
        foreach (var listener in listeners)
            listener.OnBossDead();
    }
}
