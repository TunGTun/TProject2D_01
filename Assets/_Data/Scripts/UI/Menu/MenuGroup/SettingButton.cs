using UnityEngine;

public class SettingButton : ATransitionButton
{
    [SerializeField] protected PanelCtrl panelCtrl;
    public PanelCtrl PanelCtrl => panelCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPanelCtrl();
    }

    protected virtual void LoadPanelCtrl()
    {
        if (this.panelCtrl != null) return;
        this.panelCtrl = GetComponentInParent<PanelCtrl>();
        Debug.Log(transform.name + ": LoadPanelCtrl", gameObject);
    }

    protected override void OnClickTransition()
    {
        this.Setting();
    }

    protected virtual void Setting()
    {
        this.panelCtrl.Setting.SetActive(true);
        this.panelCtrl.MenuGroup.SetActive(false);
    }
}