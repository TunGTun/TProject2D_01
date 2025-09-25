using UnityEngine;

public class BossMinotaurSkillSpawner : ABaseSpawner
{
    private static BossMinotaurSkillSpawner instance;
    public static BossMinotaurSkillSpawner Instance { get => instance; }

    public string SeismicWave = "SeismicWave";

    protected override void Awake()
    {
        base.Awake();
        if (BossMinotaurSkillSpawner.instance != null) Debug.LogError("Only 1 BossMinotaurSkillSpawner allow to exist");
        BossMinotaurSkillSpawner.instance = this;
    }
}
