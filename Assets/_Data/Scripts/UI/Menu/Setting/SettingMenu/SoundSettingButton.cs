using UnityEngine;

public class SoundSettingButton : ATransitionButton
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
        this.OpenSoundSetting();
    }

    protected virtual void OpenSoundSetting()
    {
        this.settingPanelCtrl.SoundSetting.SetActive(true);
        this.settingPanelCtrl.SettingMenu.SetActive(false);
    }
}
