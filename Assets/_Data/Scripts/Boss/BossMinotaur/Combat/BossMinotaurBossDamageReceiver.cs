using Unity.VisualScripting;
using UnityEngine;

public class BossMinotaurBossDamageReceiver : ABossDamageReceiver
{
    [Header("BossMinotaurDamageReceiver")]

    [SerializeField] protected BossMinotaurCtrl bossMinotaurCtrl;

    [Header("Flash Effect")]
    //Tam
    [SerializeField] protected Material originalMat;
    [SerializeField] protected Material hitMat;

    [SerializeField] private float flashDuration = 0.1f;
    private bool isFlashing = false;
    private float flashTimer = 0f;

    [Header("Dead")]
    [SerializeField] protected bool isDead;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossMinotaurCtrl();

        originalMat = this.baseBossCtrl.BossAnimationCtrl.SpriteRenderer.material;
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

    private void Update()
    {
        this.HandleFlash();
    }

    public override void OnDamageReceived(int damage)
    {
        this.SubHp(damage);
        this.Flash();
        if (isDead) this.OnDead();
    }

    protected virtual void Flash()
    {
        this.baseBossCtrl.BossAnimationCtrl.SpriteRenderer.material = this.hitMat;
        flashTimer = flashDuration;
        isFlashing = true;
    }

    protected virtual void HandleFlash()
    {
        if (!isFlashing) return;
        flashTimer -= Time.deltaTime;
        if (flashTimer <= 0f)
        {
            this.baseBossCtrl.BossAnimationCtrl.SpriteRenderer.material = this.originalMat;
            isFlashing = false;
        }
    }

    public override void SubHp(int damage)
    {
        base.SubHp(damage);
        this.CheckIsDead();
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
        this.bossMinotaurCtrl.BaseBossState.ChangeState(this.bossMinotaurCtrl.BaseBossState.dead);
    }
}
