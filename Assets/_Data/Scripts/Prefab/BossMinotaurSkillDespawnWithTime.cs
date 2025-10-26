using UnityEngine;

public class BossMinotaurSkillDespawnWithTime : AAutoDespawnWithTime
{
    // protected override void LoadComponents()
    // {
    //     base.LoadComponents();
    //     this.despawnTime = 4f;
    // }

    protected override void Despawn()
    {
        BossMinotaurSkillSpawner.Instance.Despawn(this.transform.parent);
    }
}
