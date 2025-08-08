using UnityEngine;

public abstract class BaseCombo : MyMonoBehaviour
{
    [Header("BossCombo")]
    [SerializeField] protected BossCtrl bossCtrl;
    public BossCtrl BossCtrl => bossCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossCtrl();
    }

    protected virtual void LoadBossCtrl()
    {
        if (bossCtrl != null) return;
        bossCtrl = GetComponentInParent<BossCtrl>();
        Debug.LogWarning(transform.name + ": LoadBossCtrl", gameObject);
    }
}
