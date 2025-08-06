using System.Collections;
using UnityEngine;

public class BossEarthSkillThree : IBossEarthSkill
{
    public IEnumerator Execute(BossCtrl bossCtrl)
    {
        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.fastJump);
        yield return new WaitForSeconds(BossData.airTimeFast);
        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.idle);

        bossCtrl.BossBaseState.ChangeState(((BossEarthState)(bossCtrl.BossBaseState)).throwRock);
        yield return new WaitForSeconds(BossData.throwInterval * BossData.throwNumber + 1f); // cần tính lại tổng thời gian ném

        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.idle);
    }
}
