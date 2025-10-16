using UnityEngine;

public class VoidRiftDespawnWithTime : AAutoDespawnWithTime
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.DespawnTime = SCharStaticData.RiftDespawnTime;
    }

    protected override void Despawn()
    {
        VoidRiftSpawner.Instance.Despawn(VoidRiftSpawner.Instance.CurrentRift);
    }
}
