using UnityEngine;

public class DummyDamageReceiver : ADamageReceiver
{
    [Header("DummyDamageReceiver")]

    [SerializeField] protected DummyCtrl dummyCtrl;
    
    [Header("Flash Effect")]
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
        
        originalMat = this.dummyCtrl.DummyAnimCtrl.SpriteRenderer.material;
    }

    protected virtual void LoadDummyCtrl()
    {
        if (dummyCtrl != null) return;
        dummyCtrl = GetComponentInParent<DummyCtrl>();
        Debug.LogWarning(transform.name + ": LoadDummyCtrl", gameObject);
    }
    
    private void Update()
    {
        this.HandleFlash();
    }

    public override void OnDamageReceived(int damage)
    {
        this.Flash();
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
}
