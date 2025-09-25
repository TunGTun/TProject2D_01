using UnityEngine;

public class SkillState : CharBaseState
{
    public IdleSkillState idleSkill;
    public DashState dash;
    public AttackState attack;
    public AttackTwoState attackTwo;

    protected override void CreateState()
    {
        idleSkill = new IdleSkillState();
        dash = new DashState();
        attack = new AttackState();
        attackTwo = new AttackTwoState();
    }
}
