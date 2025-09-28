using UnityEngine;

public class BossMinotaurData : BaseBossData
{
    protected override void Start()
    {
        base.Start();
        this.Init();
    }

    protected virtual void Init()
    {
        this.currentHealth = SBossMinotaurStaticData.MaxHP;
    }
}
