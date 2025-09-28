using UnityEngine;

public class SeismicWaveCtrl : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 3f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
