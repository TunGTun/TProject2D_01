using UnityEngine;

public class BossEarthState : BossBaseState
{
    [Header("BossEarthState")]

    public BossThrowRockState throwRock;

    protected override void CreateState()
    {
        base.CreateState();
        throwRock = new BossThrowRockState();
    }
}
