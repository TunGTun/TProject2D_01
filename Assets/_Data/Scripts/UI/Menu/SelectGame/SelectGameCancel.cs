using UnityEngine;

public class SelectGameCancel : ATransitionButton
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
        this.Cancel();
    }

    protected virtual void Cancel()
    {
        this.panelCtrl.MenuGroup.SetActive(true);
        this.panelCtrl.SelectGame.SetActive(false);
    }
}