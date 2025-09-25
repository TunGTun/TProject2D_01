using UnityEngine;

public class BossMinotaurControl : BaseBossControl
{
    protected BossMinotaurSkillOne skillOne;
    protected BossMinotaurSkillTwo skillTwo;
    protected BossMinotaurSkillThree skillThree;

    private int currentSkillIndex = 0; // 0 = skillOne, 1 = skillTwo

    protected override void Start()
    {
        base.Start();
        this.Init();
    }

    protected virtual void Init()
    {
        skillOne = new BossMinotaurSkillOne();
        skillTwo = new BossMinotaurSkillTwo();
        skillThree = new BossMinotaurSkillThree();

        this.baseBossCtrl.BaseBossState.ChangeState(this.baseBossCtrl.BaseBossState.idle);

        skillThree.Execute(this.baseBossCtrl);
    }

    private void Update()
    {

        if (skillOne != null && skillOne.IsRunning())
        {
            skillOne.Tick(this.baseBossCtrl);
        }

        if (skillTwo != null && skillTwo.IsRunning())
        {
            skillTwo.Tick(this.baseBossCtrl);
        }

        if (skillThree != null && skillThree.IsRunning())
        {
            skillThree.Tick(this.baseBossCtrl);
        }

        //if (!this.IsExecutingSkill)
        //{
        //    ExecuteNextSkill();
        //}
    }

    private void ExecuteNextSkill()
    {
        if (currentSkillIndex == 0)
        {
            skillOne.Execute(this.baseBossCtrl);
            currentSkillIndex = 1;
        }
        else
        {
            skillTwo.Execute(this.baseBossCtrl);
            currentSkillIndex = 0;
        }
    }
}
