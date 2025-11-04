using UnityEngine;

public class PauseSettingButton : ATransitionButton
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
        this.Setting();
    }

    protected virtual void Setting()
    {
        this.pausePanelCtrl.SettingPanel.SetActive(true);
        this.pausePanelCtrl.PauseMenu.SetActive(false);
    }
}
