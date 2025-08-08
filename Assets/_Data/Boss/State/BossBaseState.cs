using UnityEngine;

public abstract class BossBaseState : BaseState<BossBaseState>
{
    [Header("BossBaseState")]

    [SerializeField] protected BossCtrl bossCtrl;
    public BossCtrl BossCtrl => bossCtrl;

    public BossIdleState idle;
    public BossMoveToTargetState move;
    public BossAttackState attack;
    public BossChargeState charge;
    public BossHeavyJumpState heavyJump;
    public BossFastJumpState fastJump;

    protected override void Start()
    {
        base.Start();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.CreateState();
        this.LoadBossCtrl();
    }

    protected virtual void LoadBossCtrl()
    {
        if (bossCtrl != null) return;
        bossCtrl = GetComponentInParent<BossCtrl>();
        Debug.LogWarning(transform.name + ": LoadBossCtrl", gameObject);
    }

    protected virtual void CreateState()
    {
        idle = new BossIdleState();
        move = new BossMoveToTargetState();
        attack = new BossAttackState();
        charge = new BossChargeState();
        heavyJump = new BossHeavyJumpState();
        fastJump = new BossFastJumpState();
    }
}
