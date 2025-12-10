using UnityEngine;

public class AttackTwoState : ICharState<CharBaseState>
{
    public string Name => StateName.ATTACK_TWO_STATE;

    public FSMType FSMType => FSMType.Skill;

    protected float timer;

    private float damageDelayTimer;
    private bool hasDealtDamage = false;

    protected static readonly int enemyMask = LayerMask.GetMask("Enemy");
    protected static readonly int parryMask = LayerMask.GetMask("Enemy1", "Enemy2");

    public void OnEnter(CharBaseState context)
    {
        timer = SCharStaticData.AttackDuration;

        //damageDelayTimer = SCharStaticData.AttackDuration / 3f;
        damageDelayTimer = 0f;
        hasDealtDamage = false;

        context.CharCtrl.CharStateCtrl.FlipX();

        context.CharCtrl.AnimationCtrl.UpdateAnimation();

        AudioManager.Instance.PlaySFX(ESoundName.Attack);
    }

    public void OnExit(CharBaseState context)
    {
        context.CharCtrl.CharDamageReceiver.CanTakeDamage = true;
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        //if (isParry)
        //{
        //    parryTimer -= Time.unscaledDeltaTime;

        //    if (parryTimer <= 0f)
        //    {
        //        isParry = false;
        //        Time.timeScale = 1f;
        //        context.CharCtrl.CharDamageReceiver.CanTakeDamage = true;
        //    }

        //    return;
        //}

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
        Vector2 hitboxCenter = context.CharCtrl.PointCtrl.AttackTwoPoint.transform.position;
        Vector2 hitboxSize = new Vector2(SCharStaticData.AttackTwoSize[0], SCharStaticData.AttackTwoSize[1]);

        //Collider2D[] parryHits = Physics2D.OverlapBoxAll(hitboxCenter, hitboxSize, 0, parryMask);

        //if (parryHits.Length != 0)
        //{
        //    this.Parry(context);
        //    return;
        //}

        Collider2D[] hits = Physics2D.OverlapBoxAll(hitboxCenter, hitboxSize, 0, enemyMask);
        
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

    //private bool isParry = false;
    //private float parryTimer = 0f;
    //private const float parryDuration = 0.2f;

    protected virtual void Parry(CharBaseState context)
    {
        //AudioManager.Instance.PlaySFX(ESoundName.Parry);

        //isParry = true;
        //parryTimer = parryDuration;

        //Time.timeScale = 0;
        //context.CharCtrl.CharDamageReceiver.CanTakeDamage = false;
    }
}