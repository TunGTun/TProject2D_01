using UnityEngine;

//Tạm
//public enum BossSkill
//{
//    //Skill1,
//    Skill2,
//    Skill3,
//    Skill4
//}
//

public class BossEarthCombo : BaseCombo
{
    [Header("BossEarthCombo")]

    //Tạm
    //public BossSkill selectedSkill = BossSkill.Skill2;
    //public float skillOneCooldown;
    public float skillTwoCooldown = 3f;
    public float skillThreeCooldown = 8f;
    public float skillFourCooldown = 15f;
    //

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

        //comboRoutine = StartCoroutine(SkillLoopRoutine());

    }

    protected virtual void SetInitState()
    {
        //bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.idle);
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

    private void Update()
    {
        this.Combo();
        this.SkillTimer();
    }

    //private IEnumerator SkillLoopRoutine()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(3f);

    //        switch (selectedSkill)
    //        {
    //            //case BossSkill.Skill1:
    //            //    yield return StartCoroutine(skillOne.Execute(this.bossCtrl));
    //            //    break;
    //            case BossSkill.Skill2:
    //                yield return StartCoroutine(skillTwo.Execute(this.bossCtrl));
    //                break;
    //            case BossSkill.Skill3:
    //                yield return StartCoroutine(skillThree.Execute(this.bossCtrl));
    //                break;
    //            case BossSkill.Skill4:
    //                yield return StartCoroutine(skillFour.Execute(this.bossCtrl));
    //                break;
    //        }
    //    }
    //}

    protected virtual void Combo()
    {
        this.SkillTimer();
        if (BossData.isExecutingSkill) return;
        //bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.move);

        this.TryCombo();
    }

    protected virtual void SkillTimer()
    {
        if (BossData.skillFourTimer < skillFourCooldown)
        {
            BossData.skillFourTimer += Time.deltaTime;
        }
        if (BossData.skillThreeTimer < skillThreeCooldown)
        {
            BossData.skillThreeTimer += Time.deltaTime;
        }
        if (BossData.skillTwoTimer < skillTwoCooldown)
        {
            BossData.skillTwoTimer += Time.deltaTime;
        }
    }

    private void TryCombo()
    {
        Transform target = this.bossCtrl.Target;

        float distanceToTarget = Vector2.Distance(transform.parent.position, target.position);

        if (BossData.skillFourTimer >= skillFourCooldown)
        {
            StartCoroutine(this.skillFour.Execute(this.bossCtrl));
            return;
        }

        if (distanceToTarget <= BossData.executeSkillRange && BossData.skillThreeTimer >= skillThreeCooldown)
        {
            StartCoroutine(this.skillThree.Execute(this.bossCtrl));
            return;
        }

        if (distanceToTarget >= BossData.executeSkillRange && BossData.skillTwoTimer >= skillTwoCooldown)
        {
            StartCoroutine(this.skillTwo.Execute(this.bossCtrl));
            return;
        }

        if (distanceToTarget <= BossData.attackRange)
        {
            StartCoroutine(this.skillOne.Execute(this.bossCtrl));
            return;
        }
    }
}
