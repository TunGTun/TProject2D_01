using System.Collections;
using UnityEngine;

public class UnLinkButton : ATransitionButton
{
    [SerializeField] protected CheckPointCtrl checkPointCtrl;
    public CheckPointCtrl CheckPointCtrl => checkPointCtrl;

    protected bool isPlayerInRange = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCheckPointCtrl();
    }

    protected virtual void LoadCheckPointCtrl()
    {
        if (this.checkPointCtrl != null) return;
        this.checkPointCtrl = GetComponentInParent<CheckPointCtrl>(true);
        Debug.Log(transform.name + ": LoadCheckPointCtrl", gameObject);
    }

    protected override void OnClickTransition()
    {
        this.checkPointCtrl.CheckPointInteract.Unlink();
    }
}
