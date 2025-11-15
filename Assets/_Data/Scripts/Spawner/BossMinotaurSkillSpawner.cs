using UnityEngine;
using UnityEngine.Serialization;

public class BossMinotaurSkillSpawner : ABaseSpawner
{
    private static BossMinotaurSkillSpawner instance;
    public static BossMinotaurSkillSpawner Instance { get => instance; }

    public  string SeismicWave = "SeismicWave";
    public string FallingRock = "FallingRock";
    public string AttackOneHitBox = "AttackOneHitBox";
    public string AttackFourHitBox = "AttackFourHitBox";
    public string BossRoarEffect = "BossRoarEffect";

    protected override void Awake()
    {
        base.Awake();
        if (BossMinotaurSkillSpawner.instance != null) Debug.LogError("Only 1 BossMinotaurSkillSpawner allow to exist");
        BossMinotaurSkillSpawner.instance = this;
    }
}
