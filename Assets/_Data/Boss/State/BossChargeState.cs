using DG.Tweening;
using UnityEngine;

public class BossChargeState : IState<BossBaseState>
{
    private Color originalColor;

    public void OnEnter(BossBaseState boss)
    {
        originalColor = boss.BossCtrl.chargeSprite.color;
        boss.BossCtrl.chargeSprite.DOKill();
        boss.BossCtrl.chargeSprite
            .DOColor(BossData.chargedColor, BossData.chargeDuration)
            .SetEase(Ease.InOutSine);

        Debug.Log("BossChargeState Enter");
    }

    public void OnExit(BossBaseState boss)
    {
        boss.BossCtrl.chargeSprite.DOKill();
        boss.BossCtrl.chargeSprite.color = originalColor;

        Debug.Log("BossChargeState Exit");
    }

    public void OnFrameUpdate(BossBaseState boss)
    {

    }

    public void OnPhysicUpdate(BossBaseState boss)
    {
        Debug.Log("BossChargeState PhysicUpdate");
    }
}
