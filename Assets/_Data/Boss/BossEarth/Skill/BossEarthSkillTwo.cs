using System.Collections;
using UnityEngine;

public class BossEarthSkillTwo : IBossEarthSkill
{
    public IEnumerator Execute(BossCtrl bossCtrl)
    {
        BossData.isExecutingSkill = true;

        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.charge);

        yield return new WaitForSeconds(BossData.chargeDuration);
        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.heavyJump);

        yield return new WaitForSeconds(BossData.airTimeHeavy);
        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.idle);

        yield return new WaitForSeconds(BossData.delayAfterSkill);
        BossData.isExecutingSkill = false;
        BossData.skillTwoTimer = 0f;
    }
}
