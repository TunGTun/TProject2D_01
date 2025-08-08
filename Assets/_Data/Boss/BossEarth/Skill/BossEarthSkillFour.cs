using System.Collections;
using UnityEngine;

public class BossEarthSkillFour : IBossEarthSkill
{
    public IEnumerator Execute(BossCtrl bossCtrl)
    {
        BossData.isExecutingSkill = true;

        bossCtrl.BossBaseState.ChangeState(((BossEarthState)(bossCtrl.BossBaseState)).burrowAndRise);

        yield return new WaitForSeconds(BossData.barAnimTime * 4);
        bossCtrl.BossBaseState.ChangeState(((BossEarthState)(bossCtrl.BossBaseState)).earthSmash);

        yield return new WaitForSeconds(BossData.esAnimTime + 0.5f);
        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.idle);

        yield return new WaitForSeconds(BossData.delayAfterSkill);
        BossData.isExecutingSkill = false;
        BossData.skillFourTimer = 0f;
    }
}
