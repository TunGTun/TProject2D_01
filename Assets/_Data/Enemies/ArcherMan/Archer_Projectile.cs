using UnityEngine;

public class ArcherProjectile : MonoBehaviour
{
    public float speed = 10f;

    void Start()
    {
        // Tự bay tới phía trước
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * speed;
        // Tự hủy sau 5 giây nếu bắn trượt
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Nếu đụng Player hoặc Tường/Đất thì tự hủy
        if (hitInfo.CompareTag("Player") || hitInfo.CompareTag("Ground") || hitInfo.CompareTag("Wall"))
        {
            // Code trừ máu Player viết ở đây...
            Destroy(gameObject);
        }
    }
}