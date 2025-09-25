using UnityEngine;

public class BaseBossState : BaseState<BaseBossState>
{
    [Header("BaseBossState")]

    [SerializeField] protected BaseBossCtrl baseBossCtrl;
    public BaseBossCtrl BaseBossCtrl => baseBossCtrl;

    public BossIdleState idle;
    public BossMoveToTargetState moveToTarget;

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

    protected override void CreateState()
    {
        idle = new BossIdleState();
        moveToTarget = new BossMoveToTargetState();
    }
}
