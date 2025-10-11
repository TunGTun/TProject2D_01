using UnityEngine;

public class SkillGizmo : BaseChar
{
    void OnDrawGizmosSelected()
    {
        // Tâm hitbox
        Vector2 hitboxCenter1 = this.charCtrl.PointCtrl.AttackPointFront.position;
        float hitboxRadius1 = SCharStaticData.AttackRange / 4f;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(hitboxCenter1, hitboxRadius1);

        Vector2 hitboxCenter2 = this.charCtrl.PointCtrl.AttackPointUp.position;
        float hitboxRadius2 = SCharStaticData.AttackRange / 4f;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(hitboxCenter2, hitboxRadius2);

        Vector2 hitboxCenter3 = this.charCtrl.PointCtrl.AttackPointDown.position;
        float hitboxRadius3 = SCharStaticData.AttackRange / 4f;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(hitboxCenter3, hitboxRadius3);
    }
}
