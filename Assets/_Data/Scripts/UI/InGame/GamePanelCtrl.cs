using UnityEngine;

public class GamePanelCtrl : MyMonoBehaviour
{
    [Header("GamePanelCtrl")]
    [SerializeField] protected PausePanelCtrl pausePanelCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPausePanelCtrl();
    }

    protected virtual void LoadPausePanelCtrl()
    {
        if (this.pausePanelCtrl != null) return;
        this.pausePanelCtrl = GetComponentInChildren<PausePanelCtrl>();
        Debug.LogWarning(transform.name + ": LoadPausePanelCtrl", gameObject);
    }

    private void Update()
    {
        this.Pause();
    }

    protected virtual void Pause()
    {
        if (!InputManager.Instance.BackInput) return;
        if (CharCtrl.Instance.CharStateCtrl.StatusState.StateMachine.CurrentState == CharCtrl.Instance.CharStateCtrl.StatusState.sceneTransition) return;
        this.pausePanelCtrl.gameObject.SetActive(true);
        InputManager.Instance.SetCanControl(false);
        Time.timeScale = 0;
    }
}
