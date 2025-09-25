using UnityEngine;

public class BossMinotaurDamageReceiver : ADamageReceiver
{
    [Header("BossMinotaurDamageReceiver")]

    [SerializeField] protected BossMinotaurCtrl bossMinotaurCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossMinotaurCtrl();

    }

    protected virtual void LoadBossMinotaurCtrl()
    {
        if (bossMinotaurCtrl != null) return;
        bossMinotaurCtrl = GetComponentInParent<BossMinotaurCtrl>();
        Debug.LogWarning(transform.name + ": LoadBossMinotaurCtrl", gameObject);
    }

    protected override void LoadHitBoxCollider()
    {
        base.LoadHitBoxCollider();
        this.hitBoxCollider.isTrigger = true;
        this.hitBoxCollider.size = new Vector2(1.2f, 2.5f);
    }

    public override void OnDamageReceived(int damage)
    {
        Debug.Log("Bi danh");
        this.bossMinotaurCtrl.BaseBossData.CurrentHealth -= damage;
    }
}
