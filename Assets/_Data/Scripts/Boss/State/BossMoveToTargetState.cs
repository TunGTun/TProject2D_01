using Unity.VisualScripting;
using UnityEngine;

public class BossMoveToTargetState : IBossState<BaseBossState>
{
    public string Name => "Walk";

    private float moveTimer;

    public void OnEnter(BaseBossState boss)
    {
        moveTimer = 0f;

        boss.BaseBossCtrl.BossAnimationCtrl.ChangeAnimationState(this.Name);

        //Debug.Log("BossMoveToTargetState Enter");
    }

    public void OnExit(BaseBossState boss)
    {
        //Debug.Log("BossMoveToTargetState Exit");
    }

    public void OnFrameUpdate(BaseBossState boss)
    {
        moveTimer += Time.deltaTime;

        if (moveTimer >= SBossMinotaurStaticData.MoveToTargetMaxTime)
        {
            boss.BaseBossCtrl.BaseBossState.ChangeState(boss.BaseBossCtrl.BaseBossState.idle);
        }
    }

    public void OnPhysicUpdate(BaseBossState boss)
    {
        if (boss.BaseBossCtrl == null || boss.BaseBossCtrl.BossTarget.Target == null) return;

        Transform target = boss.BaseBossCtrl.BossTarget.Target;
        Transform bossTransform = boss.transform.parent.transform;

        float dirX = target.position.x - bossTransform.position.x;

        //if (Mathf.Abs(dirX) <= SBossMinotaurStaticData.StopDistance)
        //{
        //    boss.BaseBossCtrl.BaseBossState.ChangeState(boss.BaseBossCtrl.BaseBossState.idle);
        //    return;
        //}

        Vector3 direction = new Vector3(dirX, 0f, 0f).normalized;

        bossTransform.position += direction * BossData.moveSpeed * Time.fixedDeltaTime;

        Vector3 originalScale = bossTransform.localScale;
        if (direction.x > 0) bossTransform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        if (direction.x < 0) bossTransform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        //Debug.Log("BossMoveToTargetState PhysicUpdate");
    }
}
