using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ResetCameraDamping : MyMonoBehaviour
{
    [SerializeField] protected CinemachineConfiner2D cinemachineConfiner2D;

    [Range(0f, 5f)]
    [SerializeField] protected float originalDamping;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCinemachineConfiner2D();
    }

    protected virtual void LoadCinemachineConfiner2D()
    {
        if (cinemachineConfiner2D != null) return;
        this.cinemachineConfiner2D = GetComponent<CinemachineConfiner2D>();

        Debug.Log(transform.name + ": LoadCinemachineConfiner2D", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.originalDamping = this.cinemachineConfiner2D.Damping;
        this.cinemachineConfiner2D.Damping = 0f;
        StartCoroutine(RestoreDamping(cinemachineConfiner2D));
    }

    IEnumerator RestoreDamping(CinemachineConfiner2D _cinemachineConfiner2D)
    {
        yield return null;
        _cinemachineConfiner2D.Damping = this.originalDamping;
    }
}
