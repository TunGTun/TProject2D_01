using System.Collections;
using UnityEngine;

public class BossEarthSkillTwo : IBossEarthSkill
{
    public IEnumerator Execute(BossCtrl bossCtrl)
    {
        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.charge);

        yield return new WaitForSeconds(BossData.chargeDuration);
        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.heavyJump);

        yield return new WaitForSeconds(BossData.airTimeHeavy);
        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.idle);
    }
}
