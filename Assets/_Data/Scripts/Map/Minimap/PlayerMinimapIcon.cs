using UnityEngine;

public class PlayerMinimapIcon : MonoBehaviour
{
    public Transform player;         
    public Camera minimapCamera;      
    public RectTransform minimapUI;   
    public RectTransform iconRect;    

    void Update()
    {
        if (!minimapCamera.enabled) return;

        Vector3 viewportPos = minimapCamera.WorldToViewportPoint(player.position);

        float x = (viewportPos.x - 0.5f) * minimapUI.sizeDelta.x;
        float y = (viewportPos.y - 0.5f) * minimapUI.sizeDelta.y;

        iconRect.anchoredPosition = new Vector2(x, y);
    }
}
