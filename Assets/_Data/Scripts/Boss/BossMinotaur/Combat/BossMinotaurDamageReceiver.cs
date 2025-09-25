using UnityEngine;

public class BossMinotaurDamageReceiver : ADamageReceiver
{
    //[Header("BossMinotaurDamageReceiver")]

    protected override void LoadHitBoxCollider()
    {
        base.LoadHitBoxCollider();
        this.hitBoxCollider.isTrigger = true;
        this.hitBoxCollider.size = new Vector2(1.2f, 2.5f);
    }

    public override void OnDamageReceived()
    {
        Debug.Log("Bi danh");
    }
}
