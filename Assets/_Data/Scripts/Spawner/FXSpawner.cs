using UnityEngine;

public class FXSpawner : ABaseSpawner
{
    private static FXSpawner instance;
    public static FXSpawner Instance { get => instance; }

    public readonly string HIT = "HitEffect";
    public readonly string JUMP = "JumpEffect";
    public readonly string DOUBLE_JUMP = "DoubleJumpEffect";
    public readonly string DASH_AIR = "AirDashEffect";
    public readonly string DEAD = "DeadEffect";
    public readonly string HEAL = "HealEffect";
    public readonly string SOUL = "SoulReward";
    // public readonly string DASH_GROUND = "GroundDashEffect";

    protected override void Awake()
    {
        base.Awake();
        if (FXSpawner.instance != null) Debug.LogError("Only 1 FXSpawner allow to exist");
        FXSpawner.instance = this;
    }
}
