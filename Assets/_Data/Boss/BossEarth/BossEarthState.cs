using UnityEngine;

public class BossEarthState : BossBaseState
{
    [Header("BossEarthState")]

    public BossThrowRockState throwRock;
    public BossBurrowAndRiseState burrowAndRise;
    public BossEarthSmashState earthSmash;

    //Tạm
    public GameObject rock;
    public GameObject shockWave;
    //

    protected override void CreateState()
    {
        base.CreateState();
        throwRock = new BossThrowRockState();
        burrowAndRise = new BossBurrowAndRiseState();
        earthSmash = new BossEarthSmashState();
    }
}
