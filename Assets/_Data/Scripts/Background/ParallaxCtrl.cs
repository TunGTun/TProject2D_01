using UnityEngine;

public class ParallaxCtrl : MyMonoBehaviour
{
    [Range(0.01f, 0.05f)]
    [SerializeField] protected float parallaxSpeed;

    protected Transform cam;
    protected Vector3 camStartPos;

    protected float distance;

    protected GameObject[] layers;
    protected Material[] materials;
    protected float[] layerSpeed;

    protected float farthesBack;

    //[SerializeField] protected float layerStartZ = 10f;
    //[SerializeField] protected float layerStepZ = 15f;
    //[SerializeField] protected Vector2 layerScale = new Vector2(1.6f, 1f);

    protected override void Start()
    {
        this.Init();
    }

    protected virtual void Init()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        materials = new Material[backCount];
        layerSpeed = new float[backCount];
        layers = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            layers[i] = transform.GetChild(i).gameObject;
            //Vector3 pos = layers[i].transform.position;
            //pos.z = layerStartZ + layerStepZ * i;
            //layers[i].transform.position = pos;

            //layers[i].transform.rotation = Quaternion.Euler(90f, -180f, 0f);

            //layers[i].transform.localScale = layerScale;
            materials[i] = layers[i].GetComponent<Renderer>().material;
        }

        BackSpeedCalculate(backCount);
    }

    protected virtual void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            if ((layers[i].transform.position.z - cam.position.z) > farthesBack)
            {
                farthesBack = layers[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backCount; i++)
        {
            layerSpeed[i] = 1 - (layers[i].transform.position.z - cam.position.z) / farthesBack;
        }
    }

    private void LateUpdate()
    {
        this.UpdateParallax();
    }

    protected virtual void UpdateParallax()
    {
        this.UpdateCameraPosition();
        this.UpdateBackgroundOffset();
    }

    protected virtual void UpdateCameraPosition()
    {
        distance = cam.position.x - camStartPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);
    }

    protected virtual void UpdateBackgroundOffset()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            float speed = layerSpeed[i] * parallaxSpeed;
            materials[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}
