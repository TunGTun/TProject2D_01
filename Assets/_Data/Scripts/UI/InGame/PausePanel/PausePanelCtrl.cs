using UnityEngine;

public class PausePanelCtrl : MyMonoBehaviour
{
    [Header("PausePanelCtrl")]
    [SerializeField] protected GameObject pauseMenu;
    public GameObject PauseMenu => pauseMenu;

    [SerializeField] protected GameObject settingPanel;
    public GameObject SettingPanel => settingPanel;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPauseMenu();
        this.LoadSettingPanel();
    }

    protected virtual void LoadPauseMenu()
    {
        if (this.pauseMenu != null) return;
        this.pauseMenu = GameObject.Find("PauseMenu");
        Debug.LogWarning(transform.name + ": LoadPauseMenu", gameObject);
    }

    protected virtual void LoadSettingPanel()
    {
        if (this.settingPanel != null) return;
        this.settingPanel = GameObject.Find("SettingPanel");
        Debug.LogWarning(transform.name + ": LoadSettingPanel", gameObject);
    }
}
