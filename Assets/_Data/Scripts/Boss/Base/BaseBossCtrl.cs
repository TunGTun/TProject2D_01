using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public abstract class BaseBossCtrl : MyMonoBehaviour
{
    [Header("BaseBossCtrl")]
    [SerializeField] protected Rigidbody2D bossRigidbody2D;
    public Rigidbody2D BossRigidbody2D => bossRigidbody2D;

    [SerializeField] protected CapsuleCollider2D bossCollider2D;
    public CapsuleCollider2D Collider2D => bossCollider2D;

    //[SerializeField] protected BossStaticDataSO bossStaticDataSO;
    //public BossStaticDataSO BossStaticDataSO => bossStaticDataSO;

    [SerializeField] protected BossAnimationCtrl bossAnimationCtrl;
    public BossAnimationCtrl BossAnimationCtrl => bossAnimationCtrl;

    [SerializeField] protected BaseBossData baseBossData;
    public BaseBossData BaseBossData => baseBossData;

    [SerializeField] protected BossTarget bossTarget;
    public BossTarget BossTarget => bossTarget;

    [SerializeField] protected BaseBossState baseBossState;
    public BaseBossState BaseBossState => baseBossState;

    [SerializeField] protected BaseBossControl baseBossControl;
    public BaseBossControl BaseBossControl => baseBossControl;

    [SerializeField] protected BaseBossPointCtrl baseBossPointCtrl;
    public BaseBossPointCtrl BaseBossPointCtrl => baseBossPointCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody2D();
        this.LoadCollider2D();
        //this.LoadBossStaticDataSO();
        this.LoadBossAnimationCtrl();
        this.LoadBaseBossData();
        this.LoadBossTarget();
        this.LoadBaseBossState();
        this.LoadBaseBossControl();
        this.LoadBaseBossPointCtrl();
    }

    protected virtual void LoadRigidbody2D()
    {
        if (bossRigidbody2D != null) return;
        bossRigidbody2D = GetComponent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": LoadRigidbody2D", gameObject);
    }

    protected virtual void LoadCollider2D()
    {
        if (bossCollider2D != null) return;
        this.bossCollider2D = GetComponent<CapsuleCollider2D>();
        Debug.LogWarning(transform.name + ": LoadCollider2D", gameObject);
    }

    //protected virtual void LoadBossStaticDataSO()
    //{
    //    if (this.bossStaticDataSO != null) return;
    //    string resPath = "Boss/BossData/" + transform.name + "DataSO";
    //    this.bossStaticDataSO = Resources.Load<BossStaticDataSO>(resPath);
    //    Debug.LogWarning(transform.name + ": LoadBossStaticDataSO " + resPath, gameObject);
    //}

    protected virtual void LoadBossAnimationCtrl()
    {
        if (bossAnimationCtrl != null) return;
        bossAnimationCtrl = GetComponentInChildren<BossAnimationCtrl>();
        Debug.LogWarning(transform.name + ": LoadBossAnimationCtrl", gameObject);
    }

    protected virtual void LoadBaseBossData()
    {
        if (baseBossData != null) return;
        baseBossData = GetComponentInChildren<BaseBossData>();
        Debug.LogWarning(transform.name + ": LoadBaseBossData", gameObject);
    }

    protected virtual void LoadBossTarget()
    {
        if (bossTarget != null) return;
        bossTarget = GetComponentInChildren<BossTarget>();
        Debug.LogWarning(transform.name + ": LoadBossTarget", gameObject);
    }

    protected virtual void LoadBaseBossState()
    {
        if (baseBossState != null) return;
        baseBossState = GetComponentInChildren<BaseBossState>();
        Debug.LogWarning(transform.name + ": LoadBaseBossState", gameObject);
    }

    protected virtual void LoadBaseBossControl()
    {
        if (baseBossControl != null) return;
        baseBossControl = GetComponentInChildren<BaseBossControl>();
        Debug.LogWarning(transform.name + ": LoadBaseBossControl", gameObject);
    }

    protected virtual void LoadBaseBossPointCtrl()
    {
        if (baseBossPointCtrl != null) return;
        baseBossPointCtrl = GetComponentInChildren<BaseBossPointCtrl>();
        Debug.LogWarning(transform.name + ": LoadBaseBossPointCtrl", gameObject);
    }

    //protected abstract string GetObjectTypeString();
}
