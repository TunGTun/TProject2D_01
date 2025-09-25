using UnityEngine;

public class BossIdleState : IBossState<BaseBossState>
{
    public string Name => "Idle"; // Tạo enum

    public void OnEnter(BaseBossState boss)
    {
        boss.BaseBossCtrl.BossRigidbody2D.linearVelocity = new Vector2(0f, boss.BaseBossCtrl.BossRigidbody2D.linearVelocity.y);

        boss.BaseBossCtrl.BossAnimationCtrl.ChangeAnimationState(this.Name);

        //Debug.Log("BossIdleState Enter");
    }

    public void OnExit(BaseBossState boss)
    {
        //Debug.Log("BossIdleState Exit");
    }

    public void OnFrameUpdate(BaseBossState boss)
    {
        
    }

    public void OnPhysicUpdate(BaseBossState boss)
    {
        Transform target = boss.BaseBossCtrl.BossTarget.Target;
        Transform bossTransform = boss.transform.parent.transform;
        float dirX = target.position.x - bossTransform.position.x;
        Vector3 direction = new Vector3(dirX, 0f, 0f).normalized;
        Vector3 originalScale = bossTransform.localScale;
        if (direction.x > 0) bossTransform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        if (direction.x < 0) bossTransform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
    }
}
