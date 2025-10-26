using UnityEngine;

public class VoidRiftDespawnWithTime : AAutoDespawnWithTime
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.despawnTime = SCharStaticData.RiftDespawnTime;
    }

    protected override void Despawn()
    {
        VoidRiftSpawner.Instance.Despawn(VoidRiftSpawner.Instance.CurrentRift);
        CharCtrl.Instance.CharData.AddMP(SCharStaticData.HealMP / SCharStaticData.AttackNeedToHeal);
    }
}
