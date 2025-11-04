using UnityEngine;

public class PauseResumeButton : ABaseButton
{
    [SerializeField] protected PausePanelCtrl pausePanelCtrl;
    //public PausePanelCtrl PausePanelCtrl => pausePanelCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPausePanelCtrl();
    }

    protected virtual void LoadPausePanelCtrl()
    {
        if (this.pausePanelCtrl != null) return;
        this.pausePanelCtrl = GetComponentInParent<PausePanelCtrl>();
        Debug.Log(transform.name + ": LoadPausePanelCtrl", gameObject);
    }

    protected override void OnClick()
    {
        this.Resume();
    }

    protected virtual void Resume()
    {
        Time.timeScale = 1;
        InputManager.Instance.SetCanControl(true);
        this.pausePanelCtrl.gameObject.SetActive(false);
    }
}