using UnityEngine;

public class BossTarget : MyMonoBehaviour
{
    [Header("BossTarget")]

    [SerializeField] protected BaseBossCtrl baseBossCtrl;
    public BaseBossCtrl BaseBossCtrl => baseBossCtrl;

    [SerializeField] protected Transform target;
    public Transform Target => target;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBaseBossCtrl();
    }

    protected virtual void LoadBaseBossCtrl()
    {
        if (baseBossCtrl != null) return;
        baseBossCtrl = GetComponentInParent<BaseBossCtrl>();
        Debug.LogWarning(transform.name + ": LoadBaseBossCtrl", gameObject);
    }
}
