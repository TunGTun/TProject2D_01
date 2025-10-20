using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]

public class CharDamageReceiver : MyMonoBehaviour
{
    [Header("CharDamageReceiver")]

    [SerializeField] protected CharCtrl charCtrl;
    public CharCtrl CharCtrl => charCtrl;

    [SerializeField] protected CapsuleCollider2D hitBoxCollider;
    public CapsuleCollider2D HitBoxCollider => hitBoxCollider;

    //[Header("Flash Effect")]
    ////Tam
    //[SerializeField] protected Material originalMat;
    //[SerializeField] protected Material hitMat;

    //[SerializeField] private float flashDuration = 0.1f;
    //private bool isFlashing = false;
    //private float flashTimer = 0f;

    [Header("Knockback")]
    [SerializeField] protected float knockbackForce = 5f;
    [SerializeField] protected float knockbackUpForce = 2f;

    [Header("Dead")]
    [SerializeField] protected bool isDead;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharCtrl();
        this.LoadHitBoxCollider();

        //originalMat = this.charCtrl.AnimationCtrl.SpriteRenderer.material;
    }

    protected virtual void LoadCharCtrl()
    {
        if (charCtrl != null) return;
        charCtrl = GetComponentInParent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    protected virtual void LoadHitBoxCollider()
    {
        if (hitBoxCollider != null) return;
        this.hitBoxCollider = GetComponent<CapsuleCollider2D>();
        this.hitBoxCollider.offset = new Vector2(0f, 0f);
        this.hitBoxCollider.size = new Vector2(0.5f, 1.12f);
        this.hitBoxCollider.isTrigger = true;
        Debug.Log(transform.name + ": LoadHitBoxCollider", gameObject);
    }

    public virtual void AddHP(int hp)
    {
        this.charCtrl.CharData.CurrentHP += hp;
        if (this.charCtrl.CharData.CurrentHP > this.charCtrl.CharData.MaxHP)
            this.charCtrl.CharData.CurrentHP = this.charCtrl.CharData.MaxHP;
    }

    public virtual void SubHP(int damage)
    {
        this.charCtrl.CharData.CurrentHP -= damage;
        if (this.charCtrl.CharData.CurrentHP < 0)
            this.charCtrl.CharData.CurrentHP = 0;
    }

    //public abstract void OnDamageReceived(int damage);

    private void Update()
    {
        //this.HandleFlash();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy1"))
        {
            this.OnDamageReceived(1, collision.transform);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy2"))
        {
            this.OnDamageReceived(2, collision.transform);
        }
    }

    protected virtual void OnDamageReceived(int damage, Transform attacker)
    {
        this.SubHP(damage);
        //this.Flash();
        if (this.CheckIsDead())
        {
            this.OnDead();
            return;
        }
        this.charCtrl.CharStateCtrl.StatusState.ChangeState(this.charCtrl.CharStateCtrl.StatusState.hurt);
        this.Knockback(attacker);
    }

    //protected virtual void Flash()
    //{
    //    this.charCtrl.AnimationCtrl.SpriteRenderer.material = this.hitMat;
    //    flashTimer = flashDuration;
    //    isFlashing = true;
    //}

    //protected virtual void HandleFlash()
    //{
    //    if (!isFlashing) return;
    //    flashTimer -= Time.deltaTime;
    //    if (flashTimer <= 0f)
    //    {
    //        this.charCtrl.AnimationCtrl.SpriteRenderer.material = this.originalMat;
    //        isFlashing = false;
    //    }
    //}

    public virtual void Knockback(Transform attacker)
    {
        //if (rb == null || attacker == null) return;

        Vector2 direction = (transform.position - attacker.position).normalized;

        Vector2 knockback = new Vector2(direction.x * knockbackForce, knockbackUpForce);

         this.charCtrl.RigidBody2D.linearVelocity = Vector2.zero;
        this.charCtrl.RigidBody2D.AddForce(knockback, ForceMode2D.Impulse);
    }

    protected virtual bool CheckIsDead()
    {
        if (this.charCtrl.CharData.CurrentHP <= 0)
            this.isDead = true;
        else
            this.isDead = false;
        return this.isDead;
    }

    protected virtual void OnDead()
    {
        this.charCtrl.CharStateCtrl.StatusState.ChangeState(this.charCtrl.CharStateCtrl.StatusState.dead);
    }
}
