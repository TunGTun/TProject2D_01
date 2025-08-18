using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class CharCtrl : MyMonoBehaviour
{
    [Header("CharacterCtrl")]

    [SerializeField] protected Rigidbody2D rigidBody2D;
    public Rigidbody2D RigidBody2D => rigidBody2D;

    [SerializeField] protected BoxCollider2D charBodyBoxCollider2D;
    public BoxCollider2D CharBodyBoxCollider2D => charBodyBoxCollider2D;


    [SerializeField] protected CharState charState;
    public CharState CharState => charState;
    [SerializeField] protected CharMovement charMovement;
    public CharMovement CharMovement => charMovement;

    protected override void LoadComponents()
    {
        base.LoadComponents();;
        this.LoadRigidbody2D();
        this.LoadCharBodyBoxCollider2D();
        this.LoadCharMovement();
        this.LoadCharState();
    }

    protected virtual void LoadRigidbody2D()
    {
        if (rigidBody2D != null) return;
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        Debug.Log(transform.name + ": LoadRigidbody2D", gameObject);
    }
    protected virtual void LoadCharBodyBoxCollider2D()
    {
        if (charBodyBoxCollider2D != null) return;
        this.charBodyBoxCollider2D = GetComponent<BoxCollider2D>();
        this.charBodyBoxCollider2D.offset = new Vector2(0f, -0.03f);        
        this.charBodyBoxCollider2D.size = new Vector2(0.5f, 1f);         
        this.charBodyBoxCollider2D.isTrigger = false;                                  
        Debug.Log(transform.name + ": LoadBoxCollider2D", gameObject);
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
