using UnityEngine;

public class DummyCtrl : MyMonoBehaviour
{
    [Header("DummyCtrl")]

    [SerializeField] protected DummyAnimCtrl dummyAnimCtrl;
    public DummyAnimCtrl DummyAnimCtrl => dummyAnimCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDummyAnimCtrl();
    }

    protected virtual void LoadDummyAnimCtrl()
    {
        if (this.dummyAnimCtrl != null) return;
        this.dummyAnimCtrl = GetComponentInChildren<DummyAnimCtrl>();
        Debug.LogWarning(transform.name + ": LoadDummyAnimCtrl", gameObject);
    }
}
