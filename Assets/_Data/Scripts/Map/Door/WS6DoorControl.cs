using DG.Tweening;
using System.Collections;
using UnityEngine;

public class WS6DoorControl : MyMonoBehaviour, IBossDeathListener
{
    [SerializeField] protected float openPosY = 3.5f - 15f;
    [SerializeField] protected float closePosY = -0.5f - 15f;
    [SerializeField] protected float transitionDuration = 2f;

    [SerializeField] protected bool isCutScene = false;

    [SerializeField] protected ABossDamageReceiver bossReceiver;
    [SerializeField] protected Vector3 bossRoarOffset = new Vector3(0.73f, 0.83f, 0f);

    protected override void Start()
    {
        base.Start();
        this.Init();
        this.BossDeadObserverInit();
    }

    protected virtual void Init()
    {
        this.transform.parent.position = new Vector3(this.transform.parent.position.x, openPosY, this.transform.parent.position.z);
        if (SaveLoadSceneData.Instance.SceneData.IsDoorOpen) return;
        this.isCutScene = true;
        this.CloseDoor();
    }

    protected virtual void BossDeadObserverInit()
    {
        bossReceiver = BaseBossCtrl.Instance.ABossDamageReceiver;
        bossReceiver.RegisterListener(this);

        BaseBossCtrl.Instance.BaseBossState.ChangeState(BaseBossCtrl.Instance.BaseBossState.cutScene);
    }

    private void OnDestroy()
    {
        bossReceiver.UnregisterListener(this);
    }

    private void Update()
    {
        if (!this.isCutScene) return;
        if (CharCtrl.Instance.CharStateCtrl.StatusState.StateMachine.CurrentState != CharCtrl.Instance.CharStateCtrl.StatusState.normal) return;
        CharCtrl.Instance.CharStateCtrl.StatusState.ChangeState(CharCtrl.Instance.CharStateCtrl.StatusState.cutScene);
    }

    protected virtual void OpenDoor()
    {
        StartCoroutine(this.OpenDoorRoutine());
    }

    protected virtual IEnumerator OpenDoorRoutine()
    {
        yield return new WaitForSeconds(transitionDuration);
        this.transform.parent.DOMoveY(openPosY, transitionDuration);
    }

    protected virtual void CloseDoor()
    {
        StartCoroutine(this.CloseDoorRoutine());
    }

    protected virtual IEnumerator CloseDoorRoutine()
    {
        yield return new WaitForSeconds(SSceneTransitionData.AnimationDuration);
        this.transform.parent.DOMoveY(closePosY, transitionDuration);

        yield return new WaitForSeconds(transitionDuration);

        bossRoarOffset.x *= BaseBossCtrl.Instance.transform.localScale.x;
        Vector3 spawnPos = BaseBossCtrl.Instance.transform.position + bossRoarOffset;
        Quaternion spawnRot = Mathf.Approximately(BaseBossCtrl.Instance.transform.localScale.x, 1) ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
        BossMinotaurSkillSpawner.Instance.Spawn(BossMinotaurSkillSpawner.Instance.BossRoarEffect, spawnPos, spawnRot);

        AudioManager.Instance.PlaySFX(ESoundName.MinotaurRoar);

        yield return new WaitForSeconds(2.04f);
        this.isCutScene = false;
        CharCtrl.Instance.CharStateCtrl.StatusState.ChangeState(CharCtrl.Instance.CharStateCtrl.StatusState.normal);
        MergeCompositeToPolygon.Instance.ResetMergeComposites();
        BaseBossCtrl.Instance.BaseBossState.ChangeState(BaseBossCtrl.Instance.BaseBossState.idle);
    }

    public void OnBossDead()
    {
        this.OpenDoor();
    }
}
