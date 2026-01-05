using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeadPanelCtrl : MyMonoBehaviour
{
    [Header("DeadPanelCtrl")]
    [SerializeField] protected Image diedDarkPanel;
    public Image DiedDarkPanel => diedDarkPanel;

    [SerializeField] protected Image diedLight;
    public Image DiedLight => diedLight;

    [SerializeField] protected TextMeshProUGUI diedText;
    public TextMeshProUGUI DiedText => diedText;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDiedDarkPanel();
        this.LoadDiedLight();
        this.LoadDiedText();
    }

    protected virtual void LoadDiedDarkPanel()
    {
        if (this.diedDarkPanel != null) return;
        this.diedDarkPanel = GameObject.Find("DiedDarkPanel").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadDiedDarkPanel", gameObject);
    }

    protected virtual void LoadDiedLight()
    {
        if (this.diedLight != null) return;
        this.diedLight = GameObject.Find("DiedLight").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadDiedLight", gameObject);
    }

    protected virtual void LoadDiedText()
    {
        if (this.diedText != null) return; 
        this.diedText = GameObject.Find("DiedText").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadDiedText", gameObject);
    }
}
