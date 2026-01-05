using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharCtrl : MySingleton<CharCtrl> //Singleton chi dung ben ngoai character
{
    [Header("CharacterCtrl")]

    [SerializeField] protected Rigidbody2D rigidBody2D;
    public Rigidbody2D RigidBody2D => rigidBody2D;

    [SerializeField] protected CapsuleCollider2D charBodyCollider;
    public CapsuleCollider2D CharBodyCollider => charBodyCollider;

    [SerializeField] protected CharStateCtrl charStateCtrl;
    public CharStateCtrl CharStateCtrl => charStateCtrl;

    [SerializeField] protected CharData charData;
    public CharData CharData => charData;

    [SerializeField] protected EnvironmentChecker environmentChecker;
    public EnvironmentChecker EnvironmentChecker => environmentChecker;

    [SerializeField] protected AnimationCtrl animationCtrl;
    public AnimationCtrl AnimationCtrl => animationCtrl;

    [SerializeField] protected PointCtrl pointCtrl;
    public PointCtrl PointCtrl => pointCtrl;

    [SerializeField] protected CharDamageSender charDamageSender;
    public CharDamageSender CharDamageSender => charDamageSender;

    [SerializeField] protected CharDamageReceiver charDamageReceiver;
    public CharDamageReceiver CharDamageReceiver => charDamageReceiver;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharData();
        this.LoadRigidbody2D();
        this.LoadCharBodyCollider();
        this.LoadCharStateCtrl();
        this.LoadEnvironmentChecker();
        this.LoadAnimationCtrl();
        this.LoadPointCtrl();
        this.LoadCharDamageSender();
        this.LoadCharDamageReceiver();
    }

    protected virtual void LoadRigidbody2D()
    {
        if (rigidBody2D != null) return;
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.rigidBody2D.gravityScale = SCharStaticData.GravityScale;
        this.rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        Debug.Log(transform.name + ": LoadRigidbody2D", gameObject);
    }
    protected virtual void LoadCharBodyCollider()
    {
        if (charBodyCollider != null) return;
        this.charBodyCollider = GetComponent<CapsuleCollider2D>();
        this.charBodyCollider.offset = new Vector2(0f, 0f);        
        this.charBodyCollider.size = new Vector2(0.5f, 1.12f);         
        this.charBodyCollider.isTrigger = false;                                  
        Debug.Log(transform.name + ": LoadCharBodyCollider", gameObject);
    }

    protected virtual void LoadCharData()
    {
        if (charData != null) return;
        charData = GetComponentInChildren<CharData>();
        Debug.LogWarning(transform.name + ": LoadCharData", gameObject);
    }

    protected virtual void LoadCharStateCtrl()
    {
        if (charStateCtrl != null) return;
        charStateCtrl = GetComponentInChildren<CharStateCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharStateCtrl", gameObject);
    }

    protected virtual void LoadEnvironmentChecker()
    {
        if (environmentChecker != null) return;
        environmentChecker = GetComponentInChildren<EnvironmentChecker>();
        Debug.LogWarning(transform.name + ": LoadEnvironmentChecker", gameObject);
    }

    protected virtual void LoadAnimationCtrl()
    {
        if (animationCtrl != null) return;
        animationCtrl = GetComponentInChildren<AnimationCtrl>();
        Debug.LogWarning(transform.name + ": LoadAnimationCtrl", gameObject);
    }

    protected virtual void LoadPointCtrl()
    {
        if (pointCtrl != null) return;
        pointCtrl = GetComponentInChildren<PointCtrl>();
        Debug.LogWarning(transform.name + ": LoadPointCtrl", gameObject);
    }

    protected virtual void LoadCharDamageSender()
    {
        if (charDamageSender != null) return;
        charDamageSender = GetComponentInChildren<CharDamageSender>();
        Debug.LogWarning(transform.name + ": LoadCharDamageSender", gameObject);
    }

    protected virtual void LoadCharDamageReceiver()
    {
        if (charDamageReceiver != null) return;
        charDamageReceiver = GetComponentInChildren<CharDamageReceiver>();
        Debug.LogWarning(transform.name + ": LoadCharDamageReceiver", gameObject);
    }
}
