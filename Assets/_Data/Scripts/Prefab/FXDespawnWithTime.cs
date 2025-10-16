using UnityEngine;

public class FXDespawnWithTime : AAutoDespawnWithTime
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.DespawnTime = 1f;
    }

    protected override void Despawn()
    {
        FXSpawner.Instance.Despawn(this.transform.parent);
    }
}
