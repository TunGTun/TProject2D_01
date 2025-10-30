using UnityEngine;

public class SettingPanelCtrl : MyMonoBehaviour
{
    [SerializeField] protected GameObject settingMenu;
    public GameObject SettingMenu => settingMenu;

    [SerializeField] protected GameObject soundSetting;
    public GameObject SoundSetting => soundSetting;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSettingMenu();
        this.LoadSoundSetting();
    }

    protected virtual void LoadSettingMenu()
    {
        if (this.settingMenu != null) return;
        this.settingMenu = GameObject.Find("SettingMenu");
        Debug.Log(transform.name + ": LoadSettingMenu", gameObject);
    }

    protected virtual void LoadSoundSetting()
    {
        if (this.soundSetting != null) return;
        this.soundSetting = GameObject.Find("SoundSetting");
        Debug.Log(transform.name + ": LoadSoundSetting", gameObject);
    }
}
