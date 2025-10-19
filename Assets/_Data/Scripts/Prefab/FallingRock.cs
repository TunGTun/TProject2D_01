using UnityEngine;

public class FallingRock : MyMonoBehaviour
{
    [Header("Falling Settings")]
    public float delayBeforeFall = 1f; // Thời gian chờ trước khi rơi
    public float fallSpeed = 12f;         // Tốc độ rơi

    private Vector3 originalPosition;    // Lưu vị trí gốc
    private bool isFalling = false;      // Kiểm tra đang rơi chưa
    private float timer;

    protected override void Awake()
    {
        base.Awake();
        originalPosition = transform.localPosition;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        transform.localPosition = originalPosition;
        isFalling = false;
        timer = delayBeforeFall;
    }
    
    void Update()
    {
        if (!isFalling)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                isFalling = true;
            }
        }
        else
        {
            // Di chuyển xuống theo trục Y
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        }
    }
}