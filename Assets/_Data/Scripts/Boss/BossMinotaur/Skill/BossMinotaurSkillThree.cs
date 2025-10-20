using System;
using UnityEngine;

public class BossMinotaurSkillThree : IBossSkill
{
    protected EBossMinotaurState phase = EBossMinotaurState.None;

    public void Execute(BaseBossCtrl bossCtrl)
    {
        if (bossCtrl.BaseBossControl.IsExecutingSkill) return;

        bossCtrl.BaseBossControl.IsExecutingSkill = true;

        phase = EBossMinotaurState.AttackThree;

        bossCtrl.BaseBossState.ChangeState((bossCtrl.BaseBossState as BossMinotaurState).attackThree);
    }

    public void Tick(BaseBossCtrl bossCtrl) // update
    {
        if (phase == EBossMinotaurState.None) return;

        switch (phase)
        {
            case EBossMinotaurState.AttackThree:

                if (bossCtrl.BaseBossState.StateMachine.CurrentState != bossCtrl.BaseBossState.idle) return;
                
                phase = EBossMinotaurState.SeismicWave;

                break;

            case EBossMinotaurState.SeismicWave:

                this.SpawnSeismicWave(bossCtrl);

                this.Cancel(bossCtrl);

                break;
        }
    }

    public void Cancel(BaseBossCtrl bossCtrl)
    {
        phase = EBossMinotaurState.None;
        bossCtrl.BaseBossControl.IsExecutingSkill = false;
    }

    public bool IsRunning()
    {
        return phase != EBossMinotaurState.None;
    }

    protected virtual void SpawnSeismicWave(BaseBossCtrl bossCtrl)
    {
        Vector3 spawnPos = (bossCtrl.BaseBossPointCtrl as BossMinotaurPointCtrl).SkillThreeSpawn.transform.position;

        Transform wave = BossMinotaurSkillSpawner.Instance.Spawn(BossMinotaurSkillSpawner.Instance.SeismicWave, spawnPos, Quaternion.identity);
        wave.localScale = new Vector3(bossCtrl.transform.localScale.x * 1, 1, 1);
    }
}
