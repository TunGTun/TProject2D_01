using UnityEngine;

public class M_EnemyDamgeRecaiver : ADamageReceiver
{

    public M_Entity_AnimationTriggers entity_AnimationTriggers;

    public override void OnDamageReceived(int damage)
    {
        entity_AnimationTriggers.AttackTrigger();
        // animation
        // die=  cut
        //die ra tien
    }
}
