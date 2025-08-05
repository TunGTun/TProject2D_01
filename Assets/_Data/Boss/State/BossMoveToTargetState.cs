using UnityEngine;

public class BossMoveToTargetState : IState<BossBaseState>
{
    public void OnEnter(BossBaseState context)
    {
        Debug.Log("BossMoveToTargetState Enter");
    }

    public void OnExit(BossBaseState context)
    {
        Debug.Log("BossMoveToTargetState Exit");
    }

    public void OnFrameUpdate(BossBaseState context)
    {
        
    }

    public void OnPhysicUpdate(BossBaseState boss)
    {
        if (boss.BossCtrl == null || boss.BossCtrl.Target == null) return;

        Transform target = boss.BossCtrl.Target;
        Transform bossTransform = boss.transform.parent.transform;

        float dirX = target.position.x - bossTransform.position.x;
        Vector3 direction = new Vector3(dirX, 0f, 0f).normalized;

        bossTransform.position += direction * BossData.moveSpeed * Time.fixedDeltaTime;

        Vector3 originalScale = bossTransform.localScale;
        if (direction.x < 0) bossTransform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        if (direction.x > 0) bossTransform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        Debug.Log("BossMoveToTargetState PhysicUpdate");
    }
}
