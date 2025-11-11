using UnityEngine;

public class PanelCtrl : MyMonoBehaviour
{
    [SerializeField] protected GameObject menuGroup;
    public GameObject MenuGroup => menuGroup;

    [SerializeField] protected GameObject selectGame;
    public GameObject SelectGame => selectGame;

    [SerializeField] protected GameObject setting;
    public GameObject Setting => setting;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMenuGroup();
        this.LoadSelectGame();
        this.LoadSetting();
    }

    protected virtual void LoadMenuGroup()
    {
        if (this.menuGroup != null) return;
        this.menuGroup = GameObject.Find("MenuGroup");
        Debug.Log(transform.name + ": LoadMenuGroup", gameObject);
    }

    protected virtual void LoadSelectGame()
    {
        if (this.selectGame != null) return;
        this.selectGame = GameObject.Find("SelectGame");
        Debug.Log(transform.name + ": LoadSelectGame", gameObject);
    }

    protected virtual void LoadSetting()
    {
        if (this.setting != null) return;
        this.setting = GameObject.Find("SettingPanel");
        Debug.Log(transform.name + ": LoadSetting", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.LoadVolumeOnStart();
    }

    protected virtual void LoadVolumeOnStart()
    {
        SettingPanelCtrl settingPanelCtrl = this.setting.GetComponent<SettingPanelCtrl>();
        settingPanelCtrl.VolumeSetting.LoadVolume();
    }
}
