using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]


public class CharCtrl : MyMonoBehaviour
{
    [Header("CharacterCtrl")]
    [SerializeField] protected Rigidbody2D rigidBody2D;
    public Rigidbody2D RigidBody2D => rigidBody2D;

    [SerializeField] protected CharState charState;
    public CharState CharState => charState;
    [SerializeField] protected CharMovement charMovement;
    public CharMovement CharMovement => charMovement;
    [SerializeField] protected CharBodyCollider charBodyCollider;
    public CharBodyCollider CharBodyCollider => charBodyCollider;
    [SerializeField] protected CharGroundCollider charGroundCollider;
    public CharGroundCollider CharGroundCollider => charGroundCollider;

    protected override void LoadComponents()
    {
        base.LoadComponents();;
        this.LoadRigidbody2D();
        this.LoadCharMovement();
        this.LoadCharState();
        this.LoadCharBodyCollider();
        this.LoadCharGroundCollider();
    }

    protected virtual void LoadRigidbody2D()
    {
        if (rigidBody2D != null) return;
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

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
    protected virtual void LoadCharBodyCollider()
    {
        if (charBodyCollider != null) return;
        charBodyCollider = GetComponentInChildren<CharBodyCollider>();
        Debug.LogWarning(transform.name + ": LoadCharBodyCollider", gameObject);
    }
    protected virtual void LoadCharGroundCollider()
    {
        if (charGroundCollider != null) return;
        charGroundCollider = GetComponentInChildren<CharGroundCollider>();
        Debug.LogWarning(transform.name + ": LoadCharGroundCollider", gameObject);
    }
    

}
