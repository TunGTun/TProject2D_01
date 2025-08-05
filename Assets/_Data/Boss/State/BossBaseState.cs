using UnityEngine;

public abstract class BossBaseState : BaseState<BossBaseState>
{
    [Header("BossBaseState")]

    [SerializeField] protected BossCtrl bossCtrl;
    public BossCtrl BossCtrl => bossCtrl;

    public Transform hand;
    public Transform attackPos;

    public BossIdleState idle;
    public BossMoveToTargetState move;
    public BossAttackState attack;

    protected override void Awake()
    {
        base.Awake();
        idle = new BossIdleState();
        move = new BossMoveToTargetState();
        attack = new BossAttackState();
    }

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
