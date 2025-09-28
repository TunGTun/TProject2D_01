using System.Collections;
using UnityEngine;

public interface IBossSkill
{
    public void Execute(BaseBossCtrl bossCtrl);

    public void Tick(BaseBossCtrl bossCtrl);

    public void Cancel(BaseBossCtrl bossCtrl);

    public bool IsRunning();
}
