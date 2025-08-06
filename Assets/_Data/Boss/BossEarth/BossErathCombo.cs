using System.Collections;
using UnityEngine;

public class BossErathCombo : BaseCombo
{
    [Header("BossErathCombo")]

    private Coroutine comboRoutine;
    //private BossEarthSkillOne skillOne;
    //private BossEarthSkillTwo skillTwo;
    private BossEarthSkillThree skillThree;
    //private BossEarthSkillFour skillFour;

    protected override void Start()
    {
        base.Start();
        this.SetInitState();

        //skillOne = new BossEarthSkillOne();
        //skillTwo = new BossEarthSkillTwo();
        skillThree = new BossEarthSkillThree();
        //skillFour = new BossEarthSkillFour();

        comboRoutine = StartCoroutine(SkillLoopRoutine());
        //StartCoroutine(EnterBurrowAndRiseState());

    }

    protected virtual void SetInitState()
    {
        bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.idle);
    }

    //private IEnumerator EnterChargeState()
    //{
    //    yield return new WaitForSeconds(3f);
    //    bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.charge);
    //    StartCoroutine(EnterHeavyJumpState());
    //}

    //private IEnumerator EnterHeavyJumpState()
    //{
    //    yield return new WaitForSeconds(BossData.chargeDuration);
    //    bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.heavyJump);
    //    StartCoroutine(EnterIdleState());
    //}

    //private IEnumerator EnterIdleState()
    //{
    //    yield return new WaitForSeconds(BossData.airTimeFast);
    //    bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.idle);
    //    StartCoroutine(EnterFastJumpState());
    //}

    //private IEnumerator EnterFastJumpState()
    //{
    //    yield return new WaitForSeconds(3f);
    //    bossCtrl.BossBaseState.ChangeState(bossCtrl.BossBaseState.fastJump);
    //    StartCoroutine(EnterIdleState());
    //}

    //private IEnumerator EnterThrowRockState()
    //{
    //    yield return new WaitForSeconds(3f);
    //    bossCtrl.BossBaseState.ChangeState(((BossEarthState)(bossCtrl.BossBaseState)).throwRock);
    //    //StartCoroutine(EnterIdleState());
    //}

    //private IEnumerator EnterBurrowAndRiseState()
    //{
    //    yield return new WaitForSeconds(3f);
    //    bossCtrl.BossBaseState.ChangeState(((BossEarthState)(bossCtrl.BossBaseState)).burrowAndRise);
    //    //StartCoroutine(EnterIdleState());
    //    StartCoroutine(EnterEarthSmashStateState());
    //}

    //private IEnumerator EnterEarthSmashStateState()
    //{
    //    yield return new WaitForSeconds(4f);
    //    bossCtrl.BossBaseState.ChangeState(((BossEarthState)(bossCtrl.BossBaseState)).earthSmash);
    //    //StartCoroutine(EnterIdleState());
    //}

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
        if (skillThree == null) yield return null;
        while (true)
        {
            yield return new WaitForSeconds(3f);
            yield return StartCoroutine(skillThree.Execute(this.bossCtrl));
        }
    }
}
