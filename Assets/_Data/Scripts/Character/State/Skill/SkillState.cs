using UnityEngine;

public class SkillState : CharBaseState
{
    public IdleSkillState idleSkill;
    public DashState dash;
    public AttackState attack;

    protected override void CreateState()
    {
        idleSkill = new IdleSkillState();
        dash = new DashState();
        attack = new AttackState();
    }
}
