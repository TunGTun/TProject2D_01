using UnityEngine;
using UnityEngine.Serialization;

public class PointCtrl : BaseChar
{
    [FormerlySerializedAs("attackPointFront")]
    [Header("CharacterCtrl")]

    [SerializeField] protected Transform attackPointFront;
    public Transform AttackPointFront => attackPointFront;
    
    [SerializeField] protected Transform attackPointUp;
    public Transform AttackPointUp => attackPointUp;
    
    [SerializeField] protected Transform attackPointDown;
    public Transform AttackPointDown => attackPointDown;

    [SerializeField] protected Transform attackTwoPoint;
    public Transform AttackTwoPoint => attackTwoPoint;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAttackPointFront();
        this.LoadAttackPointUp();
        this.LoadAttackPointDown();
        this.LoadAttackTwoPoint();
    }

    protected virtual void LoadAttackPointFront()
    {
        if (attackPointFront != null) return;
        attackPointFront = transform.Find(SPointName.ATTACK_POINT_FRONT);
        attackPointFront.transform.localPosition = new Vector2(SCharStaticData.AttackOnePos[0], SCharStaticData.AttackOnePos[1]);
        Debug.LogWarning(transform.name + ": LoadAttackPointFront", gameObject);
    }
    
    protected virtual void LoadAttackPointUp()
    {
        if (attackPointUp != null) return;
        attackPointUp = transform.Find(SPointName.ATTACK_POINT_UP);
        attackPointUp.transform.localPosition = new Vector2(SCharStaticData.AttackOnePos[1], SCharStaticData.AttackOnePos[0]);
        Debug.LogWarning(transform.name + ": LoadAttackPointUp", gameObject);
    }
    
    protected virtual void LoadAttackPointDown()
    {
        if (attackPointDown != null) return;
        attackPointDown = transform.Find(SPointName.ATTACK_POINT_DOWN);
        attackPointDown.transform.localPosition = new Vector2(SCharStaticData.AttackOnePos[1], - SCharStaticData.AttackOnePos[0]);
        Debug.LogWarning(transform.name + ": LoadAttackPointDown", gameObject);
    }
    
    protected virtual void LoadAttackTwoPoint()
    {
        if (attackTwoPoint != null) return;
        attackTwoPoint = transform.Find(SPointName.ATTACK_TWO_POINT);
        attackTwoPoint.transform.localPosition = new Vector2(SCharStaticData.AttackTwoPos[0], SCharStaticData.AttackTwoPos[1]);
        Debug.LogWarning(transform.name + ": LoadAttackTwoPoint", gameObject);
    }
}
