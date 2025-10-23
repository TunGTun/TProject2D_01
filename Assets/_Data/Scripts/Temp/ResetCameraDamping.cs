using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ResetCameraDamping : MyMonoBehaviour
{
    [SerializeField] protected CinemachineCamera cinemachineCamera;
    [SerializeField] protected CinemachinePositionComposer cinemachinePositionComposer;
    [SerializeField] protected Vector3 originalDamping;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCinemachineCamera();
        this.LoadCinemachinePositionComposer();
    }

    protected virtual void LoadCinemachineCamera()
    {
        if (cinemachineCamera != null) return;
        this.cinemachineCamera = GetComponent<CinemachineCamera>();
        Debug.Log(transform.name + ": LoadCinemachineCamera", gameObject);
    }

    protected virtual void LoadCinemachinePositionComposer()
    {
        if (cinemachinePositionComposer != null) return;
        this.cinemachinePositionComposer = GetComponent<CinemachinePositionComposer>();

        Debug.Log(transform.name + ": LoadCinemachinePositionComposer", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.originalDamping = this.cinemachinePositionComposer.Damping;
        this.cinemachinePositionComposer.Damping = Vector3.zero;
        this.cinemachineCamera.PreviousStateIsValid = false;
        StartCoroutine(RestoreDamping(cinemachinePositionComposer));
    }

    IEnumerator RestoreDamping(CinemachinePositionComposer composer)
    {
        yield return null;
        //composer.Damping = this.originalDamping;
        composer.Damping = new Vector3(3f, 1f, 0f);
    }
}
