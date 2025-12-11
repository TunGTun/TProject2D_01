using UnityEngine;

public class M_OrcBat_AnimationTriggers : M_Entity_AnimationTriggers
{
    M_Enemy_OrcBat bat;
    override protected void Awake()
    {
        base.Awake();
        bat = GetComponentInParent<M_Enemy_OrcBat>();
    }

    public override void AttackTrigger()
    {
        base.AttackTrigger();
        bat.Shoot();
    }
}
