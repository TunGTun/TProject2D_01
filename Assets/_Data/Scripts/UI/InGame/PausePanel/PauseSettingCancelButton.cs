using UnityEngine;

public class PauseSettingCancelButton : ATransitionButton
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

    protected override void OnClickTransition()
    {
        this.Cancel();
    }

    protected virtual void Cancel()
    {
        this.pausePanelCtrl.PauseMenu.SetActive(true);
        this.pausePanelCtrl.SettingPanel.SetActive(false);
    }
}
