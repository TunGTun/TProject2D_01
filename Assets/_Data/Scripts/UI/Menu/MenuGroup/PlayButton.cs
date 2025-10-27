using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : ABaseButton
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

    protected override void OnClick()
    {
        this.Play();
    }

    protected virtual void Play()
    {
        this.panelCtrl.SelectGame.SetActive(true);
        this.panelCtrl.MenuGroup.SetActive(false);
    }
}
