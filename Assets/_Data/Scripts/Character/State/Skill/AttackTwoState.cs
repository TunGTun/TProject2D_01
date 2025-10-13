using UnityEngine;

public class AttackTwoState : ICharState<CharBaseState>
{
    public string Name => StateName.ATTACK_TWO_STATE;

    public FSMType FSMType => FSMType.Skill;

    protected float timer;

    private float damageDelayTimer;
    private bool hasDealtDamage = false;

    protected static readonly int enemyMask = LayerMask.GetMask("Enemy", "Enemy1", "Enemy2");
    protected static readonly Collider2D[] hitBuffer = new Collider2D[10];

    public void OnEnter(CharBaseState context)
    {
        timer = context.CharCtrl.CharData.AttackDuration;

        damageDelayTimer = context.CharCtrl.CharData.AttackDuration / 3f;
        hasDealtDamage = false;

        context.CharCtrl.CharStateCtrl.FlipX();

        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        timer -= Time.deltaTime;

        if (!hasDealtDamage)
        {
            damageDelayTimer -= Time.deltaTime;
            if (damageDelayTimer <= 0f)
            {
                DoDamage(context);
                hasDealtDamage = true;
            }
        }

        if (timer <= 0f)
        {
            context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.idleSkill);
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {

    }

    private void DoDamage(CharBaseState context)
    {
        Transform attackPoint = context.CharCtrl.PointCtrl.AttackPointFront;

        Vector2 hitboxCenter = attackPoint.position;
        float hitboxRadius = SCharStaticData.AttackRange / 4f;

        Collider2D[] hits = Physics2D.OverlapCircleAll(hitboxCenter, hitboxRadius, enemyMask);
        foreach (Collider2D hit in hits)
        {
            var receiver = hit.GetComponent<ADamageReceiver>();
            if (receiver != null)
            {
                context.CharCtrl.CharDamageSender.RegisterObserver(receiver);

                Vector2 charPos = context.CharCtrl.transform.position;
                Vector2 targetCenter = hit.bounds.center;
                Vector2 targetExtents = hit.bounds.extents;

                float sideX = Mathf.Sign(targetCenter.x - charPos.x);
                float offsetX = targetExtents.x + SCharStaticData.RiftExtraOffset;
                float spawnX = targetCenter.x + sideX * offsetX;
                Vector2 spawnPos = new Vector2(spawnX, targetCenter.y);

                VoidRiftSpawner.Instance.Spawn(VoidRiftSpawner.Instance.VoidRift, spawnPos, Quaternion.identity, true);
            }
        }

        context.CharCtrl.CharDamageSender.NotifyObservers(context.CharCtrl.CharData.AttackDamage);
        context.CharCtrl.CharDamageSender.ClearObservers();
    }
}