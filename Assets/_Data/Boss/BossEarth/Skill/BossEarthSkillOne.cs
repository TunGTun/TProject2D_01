using System.Collections;
using UnityEngine;

public class BossEarthSkillOne : IBossEarthSkill
{
    public IEnumerator Execute(BossCtrl bossCtrl)
    {
        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.move);

        yield return new WaitUntil(() =>
            Vector2.Distance(bossCtrl.transform.position, bossCtrl.Target.position) <= BossData.attackRange
        );

        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.idle);
        yield return new WaitForSeconds(BossData.waitDuration);

        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.attack);
        float totalAttackDuration = BossData.attackSpeed + BossData.waitDuration;
        yield return new WaitForSeconds(totalAttackDuration);

        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.idle);
    }
}
