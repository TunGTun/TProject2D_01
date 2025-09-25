using UnityEngine;

public class SkillState : CharBaseState
{
    public IdleSkillState idleSkill;
    public DoubleJumpState doubleJump;
    public DashState dash;
    public AttackState attack;
    public AttackTwoState attackTwo;

    protected override void CreateState()
    {
        idleSkill = new IdleSkillState();
        doubleJump = new DoubleJumpState();
        dash = new DashState();
        attack = new AttackState();
        attackTwo = new AttackTwoState();
    }
}
