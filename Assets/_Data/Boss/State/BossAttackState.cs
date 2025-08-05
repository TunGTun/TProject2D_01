using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttackState : IState<BossBaseState>
{
    private Vector3 originalPosition;

    public void OnEnter(BossBaseState boss)
    {
        originalPosition = boss.transform.parent.position;

        if (boss.BossCtrl != null && boss.BossCtrl.Target != null)
        {
            Transform target = boss.BossCtrl.Target;
            Transform bossTransform = boss.transform.parent;

            float dirX = target.position.x - bossTransform.position.x;

            Vector3 originalScale = bossTransform.localScale;
            if (dirX < 0)
                bossTransform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            else if (dirX > 0)
                bossTransform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        DG.Tweening.Sequence seq = DOTween.Sequence();

        seq.Append(boss.hand.transform.DOMove(boss.attackPos.position, BossData.attackSpeed / 2))
           .AppendInterval(BossData.waitDuration)
           .Append(boss.hand.transform.DOMove(originalPosition, BossData.attackSpeed / 2))
           .OnComplete(() => {
               boss.BossCtrl.BossBaseState.ChangeState(boss.idle);
           });

        Debug.Log("BossAttackState Enter");
    }

    public void OnExit(BossBaseState context)
    {
        Debug.Log("BossAttackState Exit");
    }

    public void OnFrameUpdate(BossBaseState context)
    {

    }

    public void OnPhysicUpdate(BossBaseState boss)
    {
        
        Debug.Log("BossAttackState PhysicUpdate");
    }
}
