using UnityEngine;

public class BossTarget : MyMonoBehaviour
{
    [Header("BossTarget")]

    [SerializeField] protected BaseBossCtrl baseBossCtrl;
    public BaseBossCtrl BaseBossCtrl => baseBossCtrl;

    [SerializeField] protected Transform target;
    public Transform Target => target;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBaseBossCtrl();
        this.LoadTarget();
        this.SetBossFacing();
    }

    protected virtual void LoadBaseBossCtrl()
    {
        if (baseBossCtrl != null) return;
        baseBossCtrl = GetComponentInParent<BaseBossCtrl>();
        Debug.LogWarning(transform.name + ": LoadBaseBossCtrl", gameObject);
    }

    protected virtual void LoadTarget()
    {
        if (target != null) return;
        target = CharCtrl.Instance.transform;
        Debug.Log(transform.name + ": LoadTarget", gameObject);
    }

    protected virtual void SetBossFacing()
    {
        Vector3 temp = this.transform.parent.localScale;
        if (this.target.transform.position.x < this.transform.parent.position.x)
            temp.x = -1;
        else
            temp.x = 1;
        this.transform.parent.localScale = temp;
    }

    private void Update()
    {
        if (this.baseBossCtrl.BossTarget != null) return;
        this.baseBossCtrl.BaseBossControl.gameObject.SetActive(false);
        //this.baseBossCtrl.BaseBossState.gameObject.SetActive(false);
    }
}
