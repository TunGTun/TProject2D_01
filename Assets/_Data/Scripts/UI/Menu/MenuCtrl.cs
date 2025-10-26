using UnityEngine;

public class MenuCtrl : MyMonoBehaviour
{
    [Header("MenuCtrl")]

    [SerializeField] protected GameObject continueButton;
    public GameObject ContinueButton => continueButton;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadContinueButton();
    }

    protected override void Start()
    {
        base.Start();
        this.continueButton.SetActive(SaveLoadManager.Instance.HasSavedFile());
    }

    protected virtual void LoadContinueButton()
    {
        if (continueButton != null) return;
        this.continueButton = GameObject.Find("Continue");
        Debug.Log(transform.name + ": LoadContinueButton", gameObject);
    }
}
