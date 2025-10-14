using Unity.Cinemachine;
using UnityEngine;

public class PlayerSpawnPoint : MyMonoBehaviour
{
    [SerializeField] protected TransitionCtrl transitionCtrl;
    public TransitionCtrl TransitionCtrl => transitionCtrl;

    protected override void Awake()
    {
        base.Awake();
        this.SetPlayerPositon();
    }

    protected virtual void SetPlayerPositon()
    {
        if (this.transitionCtrl.NextScene != MySceneManager.Instance.LastScene) return;
        this.transitionCtrl.PlayerTransform.position = this.transform.position;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTransitionCtrl();
    }

    protected virtual void LoadTransitionCtrl()
    {
        if (transitionCtrl != null) return;
        this.transitionCtrl = this.GetComponentInParent<TransitionCtrl>();
        Debug.Log(transform.name + ": LoadTransitionCtrl", gameObject);
    }
}
