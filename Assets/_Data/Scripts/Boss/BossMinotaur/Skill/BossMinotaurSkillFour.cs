using UnityEngine;

public class BossMinotaurSkillFour : IBossSkill
{
    protected EBossMinotaurState phase = EBossMinotaurState.None;

    private float delayTimer;
    private float cancelDelay = 2f;

    protected Collider2D ceilingCol;

    public void Execute(BaseBossCtrl bossCtrl)
    {
        if (bossCtrl.BaseBossControl.IsExecutingSkill) return;

        bossCtrl.BaseBossControl.IsExecutingSkill = true;

        phase = EBossMinotaurState.Taunt;

        bossCtrl.BaseBossState.ChangeState((bossCtrl.BaseBossState as BossMinotaurState).taunt);
    }

    public void Tick(BaseBossCtrl bossCtrl) // update
    {
        if (phase == EBossMinotaurState.None) return;

        switch (phase)
        {
            case EBossMinotaurState.Taunt:

                if (bossCtrl.BaseBossState.StateMachine.CurrentState != bossCtrl.BaseBossState.idle) return;

                bossCtrl.BaseBossState.ChangeState((bossCtrl.BaseBossState as BossMinotaurState).run);

                phase = EBossMinotaurState.Run;

                break;

            case EBossMinotaurState.Run:

                if (bossCtrl.BaseBossState.StateMachine.CurrentState != bossCtrl.BaseBossState.idle) return;

                phase = EBossMinotaurState.FallingRock;
                delayTimer = 0f;

                float direction = -Mathf.Sign(bossCtrl.transform.localScale.x);
                Vector2 knockback = new Vector2(direction * 2f, 0f);
                bossCtrl.BossRigidbody2D.linearVelocity = Vector2.zero;
                bossCtrl.BossRigidbody2D.AddForce(knockback, ForceMode2D.Impulse);

                ceilingCol = (bossCtrl.BaseBossEnvironmentChecker as BossMinotaurEnvironmentChecker).CeilingCol;
                this.SpawnRocks(bossCtrl);

                break;

            case EBossMinotaurState.FallingRock:

                delayTimer += Time.deltaTime;
                if (delayTimer >= cancelDelay)
                {
                    this.Cancel(bossCtrl);
                }

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

    private void SpawnRocks(BaseBossCtrl bossCtrl)
    {
        Bounds ceilingBounds = this.ceilingCol.bounds;

        Transform rockPrefab = BossMinotaurSkillSpawner.Instance.GetPrefabByName(BossMinotaurSkillSpawner.Instance.Rock).transform;

        float minX = ceilingBounds.min.x + rockPrefab.localScale.x / 2 + 1f;
        float maxX = ceilingBounds.max.x - rockPrefab.localScale.x / 2 - 1f;
        float midX = (minX + maxX) / 2f;

        float spawnY = ceilingBounds.min.y;

        float randX1 = Random.Range(minX, midX - rockPrefab.localScale.x / 2 - 1f);
        Vector3 pos1 = new Vector3(randX1, spawnY, 0);
        BossMinotaurSkillSpawner.Instance.Spawn(
            BossMinotaurSkillSpawner.Instance.Rock, pos1, Quaternion.identity
        );

        float randX2 = Random.Range(midX, maxX + rockPrefab.localScale.x / 2 + 1f);
        Vector3 pos2 = new Vector3(randX2, spawnY, 0);
        BossMinotaurSkillSpawner.Instance.Spawn(
            BossMinotaurSkillSpawner.Instance.Rock, pos2, Quaternion.identity
        );
    }
}