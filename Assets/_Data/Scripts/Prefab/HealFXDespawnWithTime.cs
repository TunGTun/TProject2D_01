using UnityEngine;

public class HealFXDespawnWithTime : AAutoDespawnWithTime
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.despawnTime = 3f;
    }

    protected override void Despawn()
    {
        FXSpawner.Instance.Despawn(this.transform.parent);
    }
}
