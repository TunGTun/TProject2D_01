using UnityEngine;

public class CameraTarget : MyMonoBehaviour
{
    [Header("CameraTarget")]

    [SerializeField] protected Transform target;
    public Transform Target => target;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTarget();
    }

    protected virtual void LoadTarget()
    {
        if (target != null) return;
        this.target = CharCtrl.Instance.transform;
        Debug.Log(transform.name + ": LoadTarget", gameObject);
    }
}
