using UnityEngine;

public class MinimapCameraZoom : MonoBehaviour
{
    public float zoomSpeed = 20f;
    public float minZoom = 20f;     // zoom gần
    public float maxZoom = 100f;    // zoom xa

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (!cam.enabled) return;

        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0)
        {
            cam.orthographicSize -= scroll * zoomSpeed * Time.deltaTime;

            cam.orthographicSize = Mathf.Clamp(
                cam.orthographicSize,
                minZoom,
                maxZoom
            );
        }
    }
}
