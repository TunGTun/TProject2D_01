using UnityEngine;

public class BaseBossControl : MyMonoBehaviour
{
    [Header("BaseBossControl")]

    [SerializeField] protected BaseBossCtrl baseBossCtrl;
    public BaseBossCtrl BaseBossCtrl => baseBossCtrl;

    public bool IsExecutingSkill { get; set; }

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
