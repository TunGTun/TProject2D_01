using System.Collections;
using UnityEngine;

//Tạm
public enum BossSkill
{
    Skill1,
    Skill2,
    Skill3,
    Skill4
}
//

public class BossErathCombo : BaseCombo
{
    [Header("BossErathCombo")]

    public BossSkill selectedSkill = BossSkill.Skill1;

    private Coroutine comboRoutine;
    private BossEarthSkillOne skillOne;
    private BossEarthSkillTwo skillTwo;
    private BossEarthSkillThree skillThree;
    private BossEarthSkillFour skillFour;

    protected override void Start()
    {
        base.Start();
        this.SetInitState();

        skillOne = new BossEarthSkillOne();
        skillTwo = new BossEarthSkillTwo();
        skillThree = new BossEarthSkillThree();
        skillFour = new BossEarthSkillFour();

        comboRoutine = StartCoroutine(SkillLoopRoutine());

    }

    protected virtual void SetInitState()
    {
        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.idle);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (comboRoutine != null)
        {
            StopCoroutine(comboRoutine);
            comboRoutine = null;
        }
    }

    private IEnumerator SkillLoopRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            switch (selectedSkill)
            {
                case BossSkill.Skill1:
                    yield return StartCoroutine(skillOne.Execute(this.bossCtrl));
                    break;
                case BossSkill.Skill2:
                    yield return StartCoroutine(skillTwo.Execute(this.bossCtrl));
                    break;
                case BossSkill.Skill3:
                    yield return StartCoroutine(skillThree.Execute(this.bossCtrl));
                    break;
                case BossSkill.Skill4:
                    yield return StartCoroutine(skillFour.Execute(this.bossCtrl));
                    break;
            }
        }
    }
}
