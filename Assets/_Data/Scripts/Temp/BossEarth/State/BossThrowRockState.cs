using UnityEngine;

public class BossThrowRockState : IState<BossBaseState>
{
    private int throwCount = 0;
    private float throwTimer = 0f;
    private bool isThrowing = false;

    public string Name => throw new System.NotImplementedException();

    public void OnEnter(BossBaseState boss)
    {
        if (boss is BossEarthState bossEarth)
        {
            Debug.Log("BossThrowRockState Enter");

            throwCount = 0;
            throwTimer = 0f;
            isThrowing = true;
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
        if (boss is BossEarthState bossEarth)
        {
            if (!isThrowing) return;

            throwTimer += Time.deltaTime;
            if (throwTimer >= BossData.throwInterval && throwCount < BossData.throwNumber)
            {
                ThrowRock(bossEarth);
                throwCount++;
                throwTimer = 0f;

                if (throwCount >= BossData.throwNumber)
                {
                    isThrowing = false;
                }
            }

            Debug.Log("BossThrowRockState FrameUpdate");
        }
    }

    public void OnPhysicUpdate(BossBaseState boss)
    {
        if (boss is BossEarthState bossEarth)
        {
            //Debug.Log("BossThrowRockState PhysicUpdate");
        }
    }

    private void ThrowRock(BossEarthState bossEarth)
    {
        if (bossEarth.rock == null || bossEarth.BossCtrl.Target == null || bossEarth.BossCtrl.hand == null) return;

        Vector2 startPos = bossEarth.BossCtrl.hand.position;
        Vector2 targetPos = bossEarth.BossCtrl.Target.position;

        GameObject rockObj = GameObject.Instantiate(
            bossEarth.rock,
            startPos,
            Quaternion.identity
        );

        RockProjectile projectile = rockObj.GetComponent<RockProjectile>();

        if (projectile != null)
        {
            projectile.Initialize(startPos, targetPos);
        }
    }
}
