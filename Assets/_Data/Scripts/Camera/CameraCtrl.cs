using Unity.Cinemachine;
using UnityEngine;

public class CameraCtrl : MyMonoBehaviour
{
    [Header("CameraCtrl")]

    [SerializeField] protected CameraTarget cameraTarget;
    public CameraTarget CameraTarget => cameraTarget;

    [SerializeField] protected CinemachineCamera cinemachineCamera;
    public CinemachineCamera CinemachineCamera => cinemachineCamera;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCameraTarget();
        this.LoadCinemachineCamera();
    }

    private void Update()
    {
        this.UpdateTarget();
    }

    protected virtual void LoadCameraTarget()
    {
        if (cameraTarget != null) return;
        this.cameraTarget = this.GetComponentInChildren<CameraTarget>();
        Debug.Log(transform.name + ": LoadCameraTarget", gameObject);
    }

    protected virtual void LoadCinemachineCamera()
    {
        if (cinemachineCamera != null) return;
        this.cinemachineCamera = GetComponentInChildren<CinemachineCamera>();
        Debug.Log(transform.name + ": LoadCinemachineCamera", gameObject);
    }

    protected virtual void UpdateTarget()
    {
        if (this.cinemachineCamera.Follow != null) return;
        this.cinemachineCamera.Follow = this.cameraTarget.Target;
    }
}
