using System.Collections;
using UnityEngine;

public class BossErathCombo : BaseCombo
{
    [Header("BossErathCombo")]

    private Coroutine comboRoutine;
    //private BossEarthSkillOne skillOne;
    //private BossEarthSkillTwo skillTwo;
    private BossEarthSkillThree skillThree;

    protected override void Start()
    {
        base.Start();
        this.SetInitState();

        //skillOne = new BossEarthSkillOne();
        //skillTwo = new BossEarthSkillTwo();
        skillThree = new BossEarthSkillThree();

        comboRoutine = StartCoroutine(SkillLoopRoutine());
        //StartCoroutine(EnterThrowRockState());

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
