using UnityEngine;

public class BossMinotaurState : BaseBossState
{
    [Header("BossMinotaurState")]

    public BossMinotaurMoveToTargetState moveToTarget;
    public BossMinotaurRunState run;

    public BossMinotaurAttackOneState attackOne;
    public BossMinotaurAttackThreeState attackThree;
    public BossMinotaurAttackFourState attackFour;

    public BossMinotaurTauntState taunt;

    protected override void CreateState()
    {
        base.CreateState();
        moveToTarget = new BossMinotaurMoveToTargetState();
        run = new BossMinotaurRunState();

        attackOne = new BossMinotaurAttackOneState();
        attackThree = new BossMinotaurAttackThreeState();
        attackFour = new BossMinotaurAttackFourState();
        
        taunt = new BossMinotaurTauntState();
    }
}
