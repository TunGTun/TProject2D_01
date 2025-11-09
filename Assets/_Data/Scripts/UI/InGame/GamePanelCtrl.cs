using DG.Tweening;
using UnityEngine;

public class GamePanelCtrl : MySingleton<GamePanelCtrl>
{
    [Header("GamePanelCtrl")]
    [SerializeField] protected PausePanelCtrl pausePanelCtrl;
    [SerializeField] protected DeadPanelCtrl deadPanelCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPausePanelCtrl();
        this.LoadDeadPanelCtrl();
    }

    protected virtual void LoadPausePanelCtrl()
    {
        if (this.pausePanelCtrl != null) return;
        this.pausePanelCtrl = GetComponentInChildren<PausePanelCtrl>();
        Debug.LogWarning(transform.name + ": LoadPausePanelCtrl", gameObject);
    }

    protected virtual void LoadDeadPanelCtrl()
    {
        if (this.deadPanelCtrl != null) return;
        this.deadPanelCtrl = GetComponentInChildren<DeadPanelCtrl>();
        Debug.LogWarning(transform.name + ": LoadDeadPanelCtrl", gameObject);
    }

    private void Update()
    {
        this.Pause();
    }

    protected virtual void Pause()
    {
        if (!InputManager.Instance.BackInput) return;
        if (CharCtrl.Instance.CharStateCtrl.StatusState.StateMachine.CurrentState == CharCtrl.Instance.CharStateCtrl.StatusState.sceneTransition) return;
        this.pausePanelCtrl.gameObject.SetActive(true);
        InputManager.Instance.SetCanControl(false);
        Time.timeScale = 0;
    }

    public virtual void EnableDeadPanel()
    {
        this.deadPanelCtrl.gameObject.SetActive(true);

        Color tempColor1 = this.deadPanelCtrl.DiedDarkPanel.color;
        tempColor1.a = 0f;
        this.deadPanelCtrl.DiedDarkPanel.color = tempColor1;
        tempColor1.a = 1f;

        Color tempColor2 = this.deadPanelCtrl.DiedLight.color;
        tempColor2.a = 0f;
        this.deadPanelCtrl.DiedLight.color = tempColor2;
        tempColor2.a = 1f;

        Color tempColor3 = this.deadPanelCtrl.DiedText.color;
        tempColor3.a = 0f;
        this.deadPanelCtrl.DiedText.color = tempColor3;
        tempColor3.a = 1f;

        this.deadPanelCtrl.DiedDarkPanel.DOColor(tempColor1, SSceneTransitionData.AnimationDuration).OnComplete(() =>
        {
            this.deadPanelCtrl.DiedLight.DOColor(tempColor2, SSceneTransitionData.AnimationDuration);
            this.deadPanelCtrl.DiedText.DOColor(tempColor3, SSceneTransitionData.AnimationDuration);
        });
    }

    public virtual void DisableDeadPanel()
    {
        Color tempColor1 = this.deadPanelCtrl.DiedDarkPanel.color;
        tempColor1.a = 1f;
        this.deadPanelCtrl.DiedDarkPanel.color = tempColor1;
        tempColor1.a = 0f;

        Color tempColor2 = this.deadPanelCtrl.DiedLight.color;
        tempColor2.a = 1f;
        this.deadPanelCtrl.DiedLight.color = tempColor2;
        tempColor2.a = 0f;

        Color tempColor3 = this.deadPanelCtrl.DiedText.color;
        tempColor3.a = 1f;
        this.deadPanelCtrl.DiedText.color = tempColor3;
        tempColor3.a = 0f;

        this.deadPanelCtrl.DiedLight.DOColor(tempColor2, SSceneTransitionData.AnimationDuration);
        this.deadPanelCtrl.DiedText.DOColor(tempColor3, SSceneTransitionData.AnimationDuration).OnComplete(() =>
        {
            this.deadPanelCtrl.DiedDarkPanel.DOColor(tempColor1, SSceneTransitionData.AnimationDuration).OnComplete(() =>
            {
                this.deadPanelCtrl.gameObject.SetActive(false);
            });
        });
    }
}
