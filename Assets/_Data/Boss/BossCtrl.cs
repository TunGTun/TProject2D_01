using UnityEngine;

public class BossCtrl : MyMonoBehaviour
{
    [Header("BossEarthCtrl")]
    public Transform Target;
    [SerializeField] protected Rigidbody2D rigidBody2D;
    public Rigidbody2D Rigidbody2D => rigidBody2D;

    [SerializeField] protected BossBaseState bossBaseState;
    public BossBaseState BossBaseState => bossBaseState;

    // Tạm
    public Transform hand;
    public Transform attackPos;
    public SpriteRenderer chargeSprite;
    public Transform leftBottomPos;
    public Transform rightBottomPos;
    public Transform centerBottomPos;
    //

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody2D();
        this.LoadBossBaseState();
    }

    protected virtual void LoadRigidbody2D()
    {
        if (rigidBody2D != null) return;
        rigidBody2D = GetComponent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": LoadRigidbody2D", gameObject);
    }

    protected virtual void LoadBossBaseState()
    {
        if (bossBaseState != null) return;
        bossBaseState = GetComponentInChildren<BossBaseState>();
        Debug.LogWarning(transform.name + ": LoadBossBaseState", gameObject);
    }
}
