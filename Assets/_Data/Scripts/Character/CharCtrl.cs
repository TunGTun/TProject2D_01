using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class CharCtrl : MyMonoBehaviour
{
    [Header("CharacterCtrl")]
    [SerializeField] protected Rigidbody2D rigidBody2D;
    public Rigidbody2D RigidBody2D => rigidBody2D;
    [SerializeField] protected BoxCollider2D boxCollider2D;
    public BoxCollider2D BoxCollider2D => boxCollider2D;
    [SerializeField] protected CharState charState;
    public CharState CharState => charState;
    [SerializeField] protected CharMovement charMovement;
    public CharMovement CharMovement => charMovement;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoxCollider2D();
        this.LoadRigidbody2D();
        this.LoadCharMovement();
        this.LoadCharState();
    }
    protected virtual void LoadBoxCollider2D()
    {
        if (boxCollider2D != null) return;
        this.boxCollider2D = GetComponent<BoxCollider2D>();
        this.boxCollider2D.offset = new Vector2(0.01792541f, -0.51429f);        // Vị trí offset của collider so với tâm object
        this.boxCollider2D.size = new Vector2(0.3403783f, 1.035643f);          // Kích thước collider
        this.boxCollider2D.isTrigger = false;                                  // Bật nếu collider là trigger (không va chạm thật)
        Debug.Log(transform.name + ": LoadBoxCollider2D", gameObject);
    }
    protected virtual void LoadRigidbody2D()
    {
        if (rigidBody2D != null) return;
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + ": LoadRigidbody2D", gameObject);
    }
    protected virtual void LoadCharState()
    {
        if (charState != null) return;
        charState = GetComponentInChildren<CharState>();
        Debug.LogWarning(transform.name + ": LoadCharState", gameObject);
    }
    protected virtual void LoadCharMovement()
    {
        if (charMovement != null) return;
        charMovement = GetComponentInChildren<CharMovement>();
        Debug.LogWarning(transform.name + ": LoadCharMovement", gameObject);
    }
    

}
