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

    protected static readonly int enemyMask = LayerMask.GetMask("Enemy", "Enemy1", "Enemy2");

    public void OnEnter(CharBaseState context)
    {
        bufferedNext = false;

        upAttack = InputManager.Instance.UpInput;
        downAttack = InputManager.Instance.DownInput &&
            context.CharCtrl.CharStateCtrl.VerticalState.StateMachine.CurrentState
            != context.CharCtrl.CharStateCtrl.VerticalState.idleGround;

        timer = SCharStaticData.AttackDuration;

        damageDelayTimer = SCharStaticData.AttackDuration / 3f;
        hasDealtDamage = false;

        if (upAttack)
        {
            context.CharCtrl.AnimationCtrl.transform.rotation = Mathf.Approximately(context.CharCtrl.transform.localScale.x, -1) ? 
                Quaternion.Euler(0f, 0f, -90f) : Quaternion.Euler(0f, 0f, 90f);
        }

        if (downAttack)
        {
            context.CharCtrl.AnimationCtrl.transform.rotation = Mathf.Approximately(context.CharCtrl.transform.localScale.x, -1) ? 
                Quaternion.Euler(0f, 0f, 90f) : Quaternion.Euler(0f, 0f, -90f);
        }

        context.CharCtrl.CharStateCtrl.FlipX();

        context.CharCtrl.AnimationCtrl.UpdateAnimation();

        AudioManager.Instance.PlaySFX(ESoundName.Attack);
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

        if (!bufferedNext && timer <= SCharStaticData.BufferWindow)
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
        Vector2 hitboxCenter = context.CharCtrl.PointCtrl.AttackPointFront.transform.position;
        Vector2 hitboxSize = new Vector2(SCharStaticData.AttackOneSize[0], SCharStaticData.AttackOneSize[1]);
        float hitboxAngle = 0f;
        
        if (upAttack)
        {
            hitboxCenter = context.CharCtrl.PointCtrl.AttackPointUp.transform.position;
            hitboxAngle = Mathf.Approximately(context.CharCtrl.transform.localScale.x, -1) ? -90f : 90f;
        }

        if (downAttack)
        {
            hitboxCenter = context.CharCtrl.PointCtrl.AttackPointDown.transform.position;
            hitboxAngle = Mathf.Approximately(context.CharCtrl.transform.localScale.x, -1) ? 90f : -90f;
        }
        
        Collider2D[] hits = Physics2D.OverlapBoxAll(hitboxCenter, hitboxSize, hitboxAngle, enemyMask);
        
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
                    VoidRiftSpawner.Instance.Spawn(VoidRiftSpawner.Instance.VoidRift, spawnPos, Quaternion.identity, false);
                }
                else
                {
                    float sideX = Mathf.Sign(targetCenter.x - charPos.x);
                    float offsetX = targetExtents.x + SCharStaticData.RiftExtraOffset;
                    float spawnX = targetCenter.x + sideX * offsetX;
                    spawnPos = new Vector2(spawnX, targetCenter.y);
                    VoidRiftSpawner.Instance.Spawn(VoidRiftSpawner.Instance.VoidRift, spawnPos, Quaternion.identity, true);
                }
            }
        }

        context.CharCtrl.CharDamageSender.NotifyObservers(context.CharCtrl.CharData.AttackDamage);
        context.CharCtrl.CharDamageSender.ClearObservers();
    }

}