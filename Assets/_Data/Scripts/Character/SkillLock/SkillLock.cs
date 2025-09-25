using System.Collections.Generic;
using UnityEngine;

public class SkillLock : MyMonoBehaviour
{
    protected HashSet<ESkill> unlockedSkills = new HashSet<ESkill>();

    protected override void Start()
    {
        base.Start();
        this.Init();
    }

    protected virtual void Init()
    {
        //this.UnlockSkill(ESkill.Dash);
        this.UnlockSkill(ESkill.DoubleJump);
    }

    public bool IsUnlocked(ESkill skill)
    {
        return unlockedSkills.Contains(skill);
    }

    public void UnlockSkill(ESkill skill)
    {
        unlockedSkills.Add(skill);
    }

    public void LockSkill(ESkill skill)
    {
        unlockedSkills.Remove(skill);
    }
}
