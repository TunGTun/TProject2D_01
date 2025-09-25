using UnityEngine;

public class BossMinotaurState : BaseBossState
{
    [Header("BossMinotaurState")]

    public BossMinotaurAttackOneState attackOne;
    public BossMinotaurAttackThreeState attackThree;
    public BossMinotaurAttackFourState attackFour;

    protected override void CreateState()
    {
        base.CreateState();
        attackOne = new BossMinotaurAttackOneState();
        attackThree = new BossMinotaurAttackThreeState();
        attackFour = new BossMinotaurAttackFourState();
    }
}
