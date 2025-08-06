using UnityEngine;

public class BossEarthState : BossBaseState
{
    [Header("BossEarthState")]

    public BossThrowRockState throwRock;

    //Tạm
    public GameObject rock;
    //

    protected override void CreateState()
    {
        base.CreateState();
        throwRock = new BossThrowRockState();
    }
}
