using DG.Tweening;
using UnityEngine;

public class BossEarthSmashState : IState<BossBaseState>
{
    public void OnEnter(BossBaseState boss)
    {
        if (boss is BossEarthState bossEarth)
        {
            Debug.Log("BossEarthSmashState Enter");

            // 1. Chuyển dần sang màu đỏ
            SpriteRenderer sr = bossEarth.BossCtrl.chargeSprite;
            Color originalColor = sr.color;
            Color redColor = Color.red;

            sr.DOColor(redColor, BossData.esAnimTime).OnComplete(() =>
            {
                SpawnShockwaves(bossEarth);

                sr.color = originalColor;
            });
        }
    }

    private void SpawnShockwaves(BossEarthState bossEarth)
    {
        Transform spawnPoint = bossEarth.BossCtrl.transform;
        GameObject shockwavePrefab = bossEarth.shockWave;

        GameObject left = GameObject.Instantiate(shockwavePrefab, spawnPoint.position, Quaternion.identity);
        left.GetComponent<ShockwaveMover>().Init(Vector2.left);

        GameObject right = GameObject.Instantiate(shockwavePrefab, spawnPoint.position, Quaternion.identity);
        right.GetComponent<ShockwaveMover>().Init(Vector2.right);
    }

    public void OnExit(BossBaseState boss)
    {
        if (boss is BossEarthState bossEarth)
        {
            Debug.Log("BossEarthSmashState Exit");
        }
    }

    public void OnFrameUpdate(BossBaseState boss)
    {
        if (boss is BossEarthState bossEarth)
        {

            Debug.Log("BossEarthSmashState FrameUpdate");
        }
    }

    public void OnPhysicUpdate(BossBaseState boss)
    {
        if (boss is BossEarthState bossEarth)
        {
            //Debug.Log("BossEarthSmashState PhysicUpdate");
        }
    }
}
