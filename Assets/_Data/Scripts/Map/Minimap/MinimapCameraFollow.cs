using UnityEngine;

public class MinimapCameraFollow : MyMonoBehaviour
{
    public Transform player;
    public float height = -20f; 

    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 pos = player.position;
        pos.z = height;     
        transform.position = pos;
    }
}
