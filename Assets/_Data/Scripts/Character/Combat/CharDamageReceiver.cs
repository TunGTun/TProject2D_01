using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]

public class CharDamageReceiver : MyMonoBehaviour
{
    [Header("CharDamageReceiver")]

    [SerializeField] protected CharCtrl charCtrl;
    public CharCtrl CharCtrl => charCtrl;

    [SerializeField] protected CapsuleCollider2D hitBoxCollider;
    public CapsuleCollider2D HitBoxCollider => hitBoxCollider;

    [SerializeField] protected bool canTakeDamage = true;
    public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }

    //[Header("Flash Effect")]
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

    private void Update()
    {
        //this.HandleFlash();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canTakeDamage) return;
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
        this.charCtrl.CharData.SubHP(damage);
        //this.Flash();

        this.charCtrl.CharStateCtrl.StatusState.ChangeState(this.charCtrl.CharStateCtrl.StatusState.hurt);
        this.Knockback(attacker);

        if (this.CheckIsDead())
        {
            this.OnDead();
            return;
        }
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
        Vector2 direction = (transform.position - attacker.position).normalized;

        Vector2 knockback = new Vector2(direction.x * knockbackForce, knockbackUpForce);

         this.charCtrl.RigidBody2D.linearVelocity = Vector2.zero;
        this.charCtrl.RigidBody2D.AddForce(knockback, ForceMode2D.Impulse);
    }

    protected virtual bool CheckIsDead()
    {
        if (this.charCtrl.CharData.CurrentHP == 0)
            this.isDead = true;
        else
            this.isDead = false;
        return this.isDead;
    }

    protected virtual void OnDead()
    {
        StartCoroutine(OnDeadRoutine());
    }

    protected virtual IEnumerator OnDeadRoutine()
    {
        this.charCtrl.CharStateCtrl.StatusState.ChangeState(this.charCtrl.CharStateCtrl.StatusState.dead);

        yield return new WaitForSeconds(1f);
        GamePanelCtrl.Instance.EnableDeadPanel();

        yield return new WaitForSeconds(3f);
        GamePanelCtrl.Instance.DisableDeadPanel();
        CheckPointData checkPointData = SaveLoadManager.Instance.PlayerData.LastCheckPoint;
        this.charCtrl.CharData.AddHP(this.charCtrl.CharData.MaxHP);
        MySceneManager.Instance.LoadScene(checkPointData.SceneName);

        yield return new WaitUntil(() => MySceneManager.Instance.GetCurrentSceneName() == checkPointData.SceneName);
        this.transform.parent.position = checkPointData.SpawnPoint;
        SaveLoadManager.Instance.SavePlayer();

        yield return new WaitForSeconds(SSceneTransitionData.AnimationDuration);
        this.charCtrl.CharStateCtrl.StatusState.ChangeState(this.charCtrl.CharStateCtrl.StatusState.spawn);
    }
}
