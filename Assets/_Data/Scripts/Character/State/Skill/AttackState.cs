using UnityEngine;

public class AttackState : ICharState<CharBaseState>
{
    public string Name => StateName.ATTACK_ONE_STATE;

    public FSMType FSMType => FSMType.Skill;

    protected float timer;
    private bool bufferedNext;

    private float damageDelayTimer;
    private bool hasDealtDamage;

    protected bool upAttack = false;
    protected bool downAttack = false;

    protected static readonly int enemyMask = LayerMask.GetMask("Enemy");
    protected static readonly Collider2D[] hitBuffer = new Collider2D[10];

    public void OnEnter(CharBaseState context)
    {
        bufferedNext = false;

        upAttack = InputManager.Instance.UpInput;
        downAttack = InputManager.Instance.DownInput &&
            context.CharCtrl.CharStateCtrl.VerticalState.StateMachine.CurrentState
            != context.CharCtrl.CharStateCtrl.VerticalState.idleGround;

        timer = context.CharCtrl.CharData.AttackDuration;

        damageDelayTimer = context.CharCtrl.CharData.AttackDuration / 3f;
        hasDealtDamage = false;

        if (upAttack)
        {
            if (context.CharCtrl.transform.localScale.x == -1)
                context.CharCtrl.AnimationCtrl.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            else
                context.CharCtrl.AnimationCtrl.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }

        if (downAttack)
        {
            if (context.CharCtrl.transform.localScale.x == -1)
                context.CharCtrl.AnimationCtrl.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            else
                context.CharCtrl.AnimationCtrl.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }

        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        context.CharCtrl.AnimationCtrl.transform.rotation = Quaternion.identity;
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

        if (!bufferedNext && timer <= context.CharCtrl.CharData.BufferWindow)
        {
            if (InputManager.Instance.AttackInput)
            {
                bufferedNext = true;
            }
        }

        if (timer <= 0f)
        {
            if (bufferedNext && !(upAttack || downAttack))
                context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.attackTwo);
            else
                context.CharCtrl.CharStateCtrl.SkillState.ChangeState(context.CharCtrl.CharStateCtrl.SkillState.idleSkill);
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {

    }

    private void DoDamage(CharBaseState context)
    {
        Transform attackPoint = context.CharCtrl.PointCtrl.AttackPointFront;

        if (upAttack)
        {
            attackPoint = context.CharCtrl.PointCtrl.AttackPointUp;
        }

        if (downAttack)
        {
            attackPoint = context.CharCtrl.PointCtrl.AttackPointDown;
        }

        Vector2 hitboxCenter = attackPoint.position;
        float hitboxRadius = SCharStaticData.AttackRange / 4f;

        Collider2D[] hits = Physics2D.OverlapCircleAll(hitboxCenter, hitboxRadius, LayerMask.GetMask("Enemy"));
        foreach (Collider2D hit in hits)
        {
            var receiver = hit.GetComponent<ADamageReceiver>();
            if (receiver != null)
            {
                context.CharCtrl.CharDamageSender.RegisterObserver(receiver);

                Vector2 charPos = context.CharCtrl.transform.position;
                Vector2 targetCenter = hit.bounds.center;
                Vector2 targetExtents = hit.bounds.extents;

                Vector2 spawnPos;

                if (upAttack || downAttack)
                {
                    float sideY = Mathf.Sign(targetCenter.y - charPos.y);
                    float offsetY = targetExtents.y + SCharStaticData.RiftExtraOffset;
                    float spawnY = targetCenter.y + sideY * offsetY;
                    spawnPos = new Vector2(targetCenter.x, spawnY);
                }
                else
                {
                    float sideX = Mathf.Sign(targetCenter.x - charPos.x);
                    float offsetX = targetExtents.x + SCharStaticData.RiftExtraOffset;
                    float spawnX = targetCenter.x + sideX * offsetX;
                    spawnPos = new Vector2(spawnX, targetCenter.y);
                }

                VoidRiftSpawner.Instance.Spawn("VoidRift", spawnPos, Quaternion.identity);
            }
        }

        context.CharCtrl.CharDamageSender.NotifyObservers(context.CharCtrl.CharData.AttackDamage);
        context.CharCtrl.CharDamageSender.ClearObservers();
    }

}