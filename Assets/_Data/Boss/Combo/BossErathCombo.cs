using System.Collections;
using UnityEngine;

public class BossErathCombo : BaseCombo
{
    private Coroutine skillOneRoutine;

    protected override void Start()
    {
        base.Start();
        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.idle);
        this.SkillOne();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (skillOneRoutine != null)
        {
            StopCoroutine(skillOneRoutine);
            skillOneRoutine = null;
        }
    }

    public void SkillOne()
    {
        if (skillOneRoutine != null)
        {
            StopCoroutine(skillOneRoutine);
        }

        skillOneRoutine = StartCoroutine(SkillOneRoutine());
    }

    private IEnumerator SkillOneRoutine()
    {
        while (true)
        {
            // 1. Di chuyển đến target
            bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.move);

            yield return new WaitUntil(() =>
                Vector2.Distance(bossCtrl.transform.position, bossCtrl.Target.position) <= BossData.attackRange
            );

            // 2. Idle trước khi tấn công
            bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.idle);
            yield return new WaitForSeconds(BossData.waitDuration);

            // 3. Tấn công
            bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.attack);

            float totalAttackDuration = BossData.attackSpeed + BossData.waitDuration;
            yield return new WaitForSeconds(totalAttackDuration);

            // 4. Idle sau khi tấn công
            bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.idle);
            yield return new WaitForSeconds(BossData.waitDuration);
        }
    }
}
