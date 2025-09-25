using UnityEngine;

public class BossMinotaurPointCtrl : BaseBossPointCtrl
{

    [Header("BossMinotaurPointCtrl")]

    [SerializeField] protected Transform skillThreeSpawn;
    public Transform SkillThreeSpawn => skillThreeSpawn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSkillThreeSpawn();
    }

    protected virtual void LoadSkillThreeSpawn()
    {
        if (skillThreeSpawn != null) return;
        skillThreeSpawn = transform.Find("BossMinotaurSkillThreeSpawn");
        Debug.LogWarning(transform.name + ": LoadSkillThreeSpawn", gameObject);
    }
}
