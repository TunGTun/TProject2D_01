using Unity.VisualScripting;
using UnityEngine;

//[ExecuteAlways]
public class ParallaxRepeater : MyMonoBehaviour
{
    [SerializeField] protected Transform cameraTransform;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField, Range(0f, 1f)] protected float parallaxFactor = 0.5f;

    protected float textureUnitSizeX;
    protected Vector2 startPos;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCameraTransform();
        this.LoadSpriteRenderer();
    }

    protected virtual void LoadCameraTransform()
    {
        if (cameraTransform != null) return;
        cameraTransform = Camera.main.transform;
        Debug.LogWarning(transform.name + ": LoadCameraTransform", gameObject);
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (spriteRenderer != null) return;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.LogWarning(transform.name + ": LoadSpriteRenderer", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.Init();
    }

    protected virtual void Init()
    {
        startPos = transform.position;
        textureUnitSizeX = this.spriteRenderer.bounds.size.x;
    }


    private void LateUpdate()
    {
        this.UpdateParallaxPosition();
        this.CheckAndRepeat();
    }

    protected virtual void UpdateParallaxPosition()
    {
        float deltaX = cameraTransform.position.x * parallaxFactor;
        Vector2 newPos = new Vector2(startPos.x + deltaX, startPos.y);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }

    protected virtual void CheckAndRepeat()
    {
        float camDistance = cameraTransform.position.x - transform.position.x;

        if (Mathf.Abs(camDistance) >= textureUnitSizeX)
        {
            float offset = camDistance % textureUnitSizeX;
            startPos.x += offset;
        }
    }
}
