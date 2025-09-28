using UnityEngine;

public class BossMinotaurPointCtrl : BaseBossPointCtrl
{

    [Header("BossMinotaurPointCtrl")]

    [SerializeField] protected Transform attackOneHitBox;
    public Transform AttackOneHitBox => attackOneHitBox;

    [SerializeField] protected Transform attackFourHitBox;
    public Transform AttackFourHitBox => attackFourHitBox;

    [SerializeField] protected Transform skillThreeSpawn;
    public Transform SkillThreeSpawn => skillThreeSpawn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAttackOneHitBox();
        this.LoadAttackFourHitBox();
        this.LoadSkillThreeSpawn();
    }

    protected virtual void LoadAttackOneHitBox()
    {
        if (attackOneHitBox != null) return;
        attackOneHitBox = transform.Find("BossMinotaurAttackOneHitBox");
        attackOneHitBox.transform.localPosition = new Vector2(1.01f, 0.29f);
        Debug.LogWarning(transform.name + ": LoadAttackOneHitBox", gameObject);
    }

    protected virtual void LoadAttackFourHitBox()
    {
        if (attackFourHitBox != null) return;
        attackFourHitBox = transform.Find("BossMinotaurAttackFourHitBox");
        attackFourHitBox.transform.localPosition = new Vector2(0f, -1f);
        Debug.LogWarning(transform.name + ": LoadAttackFourHitBox", gameObject);
    }

    protected virtual void LoadSkillThreeSpawn()
    {
        if (skillThreeSpawn != null) return;
        skillThreeSpawn = transform.Find("BossMinotaurSkillThreeSpawn");
        skillThreeSpawn.transform.localPosition = new Vector2(0.6f, -1.25f);
        Debug.LogWarning(transform.name + ": LoadSkillThreeSpawn", gameObject);
    }
}
