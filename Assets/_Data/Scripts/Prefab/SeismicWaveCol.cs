using UnityEngine;

public class SeismicWaveCol : MyMonoBehaviour
{
    public float speed = 5f;
    private Vector3 startPos;

    protected override void Awake()
    {
        base.Awake();
        startPos = transform.localPosition * Mathf.Sign(transform.parent.localScale.x);
    }

    protected override void OnEnable()
    {  
        base.OnEnable();
        transform.localPosition = startPos;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}