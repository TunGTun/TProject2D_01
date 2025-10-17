using UnityEngine;

public class DummyDamageReceiver : ADamageReceiver
{
    [Header("DummyDamageReceiver")]

    [SerializeField] protected DummyCtrl dummyCtrl;
    [SerializeField] protected Collider2D damageReceiverCollider;
    //Tam
    [SerializeField] protected Material originalMat;
    [SerializeField] protected Material hitMat;

    [SerializeField] private float flashDuration = 0.1f;
    private bool isFlashing = false;
    private float flashTimer = 0f;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDummyCtrl();
        this.LoadDamageReceiverCollider();
        
        originalMat = this.dummyCtrl.DummyAnimCtrl.SpriteRenderer.material;
    }

    protected virtual void LoadDummyCtrl()
    {
        if (dummyCtrl != null) return;
        dummyCtrl = GetComponentInParent<DummyCtrl>();
        Debug.LogWarning(transform.name + ": LoadDummyCtrl", gameObject);
    }
    
    protected virtual void LoadDamageReceiverCollider()
    {
        if (damageReceiverCollider != null) return;
        damageReceiverCollider = GetComponent<Collider2D>();
        Debug.LogWarning(transform.name + ": LoadDamageReceiverCollider", gameObject);
    }
    
    private void Update()
    {
        this.HandleFlash();
    }

    public override void OnDamageReceived(int damage)
    {
        this.Flash();
        this.HitEffect();
    }

    protected virtual void Flash()
    {
        this.dummyCtrl.DummyAnimCtrl.SpriteRenderer.material = this.hitMat;
        flashTimer = flashDuration;
        isFlashing = true;
    }

    protected virtual void HandleFlash()
    {
        if (!isFlashing) return;
        flashTimer -= Time.deltaTime;
        if (flashTimer <= 0f)
        {
            this.dummyCtrl.DummyAnimCtrl.SpriteRenderer.material = this.originalMat;
            isFlashing = false;
        }
    }

    protected virtual void HitEffect()
    {
        Vector3 spawnPos = this.damageReceiverCollider.bounds.center;
        Quaternion spawnRot = Quaternion.identity;
        FXSpawner.Instance.Spawn(FXSpawner.Instance.HIT, spawnPos, spawnRot);
    }
    
}
