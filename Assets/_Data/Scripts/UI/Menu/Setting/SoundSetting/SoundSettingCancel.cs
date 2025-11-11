using UnityEngine;

public class SoundSettingCancel : ATransitionButton
{
    [SerializeField] protected SettingPanelCtrl settingPanelCtrl;
    public SettingPanelCtrl SettingPanelCtrl => settingPanelCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSettingPanelCtrl();
    }

    protected virtual void LoadSettingPanelCtrl()
    {
        if (this.settingPanelCtrl != null) return;
        this.settingPanelCtrl = GetComponentInParent<SettingPanelCtrl>();
        Debug.Log(transform.name + ": LoadSettingPanelCtrl", gameObject);
    }

    protected override void OnClickTransition()
    {
        this.Cancel();
    }

    protected virtual void Cancel()
    {
        this.settingPanelCtrl.SettingMenu.SetActive(true);
        this.settingPanelCtrl.SoundSetting.SetActive(false);
    }
}
