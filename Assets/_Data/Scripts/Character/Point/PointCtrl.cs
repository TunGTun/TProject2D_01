using UnityEngine;

public class PointCtrl : BaseChar
{
    [Header("CharacterCtrl")]

    [SerializeField] protected Transform attackPointFront;
    public Transform AttackPointFront => attackPointFront;

    [SerializeField] protected Transform attackPointUp;
    public Transform AttackPointUp => attackPointUp;

    [SerializeField] protected Transform attackPointDown;
    public Transform AttackPointDown => attackPointDown;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAttackPointFront();
        this.LoadAttackPointUp();
        this.LoadAttackPointDown();
    }

    protected virtual void LoadAttackPointFront()
    {
        if (attackPointFront != null) return;
        attackPointFront = transform.Find(SPointName.ATTACK_POINT_FRONT);
        Debug.LogWarning(transform.name + ": LoadAttackPointFront", gameObject);
    }

    protected virtual void LoadAttackPointUp()
    {
        if (attackPointUp != null) return;
        attackPointUp = transform.Find(SPointName.ATTACK_POINT_UP);
        Debug.LogWarning(transform.name + ": LoadAttackPointUp", gameObject);
    }


    protected virtual void LoadAttackPointDown()
    {
        if (attackPointDown != null) return;
        attackPointDown = transform.Find(SPointName.ATTACK_POINT_DOWN);
        Debug.LogWarning(transform.name + ": LoadAttackPointDown", gameObject);
    }
}
