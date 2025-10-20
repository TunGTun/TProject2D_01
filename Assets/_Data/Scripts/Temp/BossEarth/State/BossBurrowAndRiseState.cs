using DG.Tweening;
using UnityEngine;

public class BossBurrowAndRiseState : IState<BossBaseState>
{
    public string Name => "";

    public void OnEnter(BossBaseState boss)
    {
        if (boss is BossEarthState bossEarth)
        {
            Debug.Log("BossBurrowAndRiseState Enter");

            Transform bossTransform = bossEarth.transform.parent;
            Rigidbody2D rb = bossEarth.BossCtrl.Rigidbody2D;

            Vector3 originalScale = bossTransform.localScale;
            Vector3 centerBottomPos = bossEarth.BossCtrl.centerBottomPos.transform.position;
            float originalGravityScale = rb.gravityScale;

            float originalY = bossTransform.position.y;

            Sequence seq = DOTween.Sequence();

            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;

            Vector3 shrinkScale = Vector3.zero;
            seq.Append(bossTransform.DOScale(shrinkScale, BossData.barAnimTime));

            seq.AppendInterval(BossData.barAnimTime);

            seq.AppendCallback(() =>
            {
                bossTransform.position = new Vector3(centerBottomPos.x, originalY, centerBottomPos.z);
            });

            seq.AppendInterval(BossData.barAnimTime);

            seq.Append(bossTransform.DOScale(originalScale, BossData.barAnimTime));

            seq.AppendCallback(() =>
            {
                rb.gravityScale = originalGravityScale;
            });
        }
    }

    public void OnExit(BossBaseState boss)
    {
        if (boss is BossEarthState bossEarth)
        {
            Debug.Log("BossBurrowAndRiseState Exit");
        }
    }

    public void OnFrameUpdate(BossBaseState boss)
    {
        if (boss is BossEarthState bossEarth)
        {

            Debug.Log("BossBurrowAndRiseState FrameUpdate");
        }
    }

    public void OnPhysicUpdate(BossBaseState boss)
    {
        if (boss is BossEarthState bossEarth)
        {
            //Debug.Log("BossBurrowAndRiseState PhysicUpdate");
        }
    }
}
