using UnityEngine;

public class BossThrowRockState : IState<BossBaseState>
{
    public void OnEnter(BossBaseState boss)
    {
        if (boss is BossEarthState bossEarth)
        {
            Debug.Log("BossThrowRockState Enter");
        }
    }

    public void OnExit(BossBaseState boss)
    {
        if (boss is BossEarthState bossEarth)
        {
            Debug.Log("BossThrowRockState Exit");
        }
    }

    public void OnFrameUpdate(BossBaseState boss)
    {

    }

    public void OnPhysicUpdate(BossBaseState boss)
    {
        if (boss is BossEarthState bossEarth)
        {
            Debug.Log("BossThrowRockState PhysicUpdate");
        }
    }
}
