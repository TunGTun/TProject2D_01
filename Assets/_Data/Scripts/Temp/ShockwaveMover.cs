using UnityEngine;

public class ShockwaveMover : MonoBehaviour
{
    public float speed = 5f;
    public float destroyDuration = 10f;
    private Vector2 direction;

    public void Init(Vector2 moveDirection)
    {
        direction = moveDirection.normalized;
        Destroy(gameObject, destroyDuration);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
