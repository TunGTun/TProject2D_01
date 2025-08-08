using DG.Tweening;
using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    [Header("Physics")]
    public float gravity = -9.8f;
    public float flightTime = 1.0f;  // Thời gian bay đến target
    public float destroyAfter = 5f;

    private Vector2 velocity;
    private float elapsedTime = 0f;

    [Header("Rotation")]
    public float rotationSpeed = 360f;

    public void Initialize(Vector2 startPos, Vector2 targetPos)
    {
        transform.position = startPos;

        // Tính khoảng cách
        Vector2 displacement = targetPos - startPos;
        float dx = displacement.x;
        float dy = displacement.y;
        float t = flightTime;

        // Tính vận tốc theo công thức vật lý
        float vx = dx / t;
        float vy = (dy - 0.5f * gravity * t * t) / t;

        velocity = new Vector2(vx, vy);
        elapsedTime = 0f;

        // Hủy sau X giây
        Destroy(gameObject, destroyAfter);

        // Quay tròn
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
        elapsedTime += Time.deltaTime;

        // Áp dụng trọng lực
        velocity.y += gravity * Time.deltaTime;

        // Di chuyển
        transform.position += (Vector3)(velocity * Time.deltaTime);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
