using UnityEngine;

public class M_ArcherMan_AnimationTriggers : M_Entity_AnimationTriggers
{
    M_Enemy_ArcherMan archerMan;
    override protected void Awake()
    {
        base.Awake();
        archerMan = GetComponentInParent<M_Enemy_ArcherMan>();
    }

    public override void AttackTrigger()
    {
        base.AttackTrigger();
        archerMan.Shoot();
    }
}
