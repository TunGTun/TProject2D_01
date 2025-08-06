using DG.Tweening;
using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;
    public float destroyDuration = 5f;
    private Vector2 direction;

    [Header("Rotation")]
    public float rotationSpeed = 360f;

    public void Initialize(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, destroyDuration);

        // Bắt đầu quay
        transform.DORotate(
            new Vector3(0, 0, -rotationSpeed),
            1f,
            RotateMode.FastBeyond360
        )
        .SetEase(Ease.Linear)
        .SetLoops(-1);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnDestroy()
    {
        // Hủy Tween khi đối tượng bị hủy để tránh memory leak
        transform.DOKill();
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    // Xử lý va chạm (nếu cần)
    //    // if (other.CompareTag("Player")) { ... }

    //    Destroy(gameObject);
    //}
}
