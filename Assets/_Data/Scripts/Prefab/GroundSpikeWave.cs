using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpikeWave : MonoBehaviour
{
    [Header("Position Settings")]
    public float spacingPosToNeg = 0.8f;   // Khoảng cách giữa cọc scale.x = 1 → -1
    public float spacingNegToPos = 0.3f;   // Khoảng cách giữa cọc scale.x = -1 → 1
    public float yPos = 0f;

    [Header("Rotation Settings")]
    public float rotMin = -10f;
    public float rotMax = -20f;

    [Header("Movement Settings")]
    public float moveTimeMin = 0.3f;
    public float moveTimeMax = 0.5f;
    public float delayBetween = 0.1f;
    public float speed = 3.5f;
    public int nActive = 5; // Số cọc có thể bật cùng lúc

    private List<Transform> spikes = new List<Transform>();
    private List<Vector3> startPositions = new List<Vector3>();
    private List<Vector3> topPositions = new List<Vector3>();
    private Coroutine waveRoutine;

    void Awake()
    {
        spikes.Clear();
        foreach (Transform child in transform)
            spikes.Add(child);
    }

    void OnEnable()
    {
        ArrangeSpikes();
        if (waveRoutine != null)
            StopCoroutine(waveRoutine);
        waveRoutine = StartCoroutine(WaveOnceRoutine());
    }

    void OnDisable()
    {
        if (waveRoutine != null)
            StopCoroutine(waveRoutine);
        waveRoutine = null;
    }

    void ArrangeSpikes()
    {
        if (spikes.Count == 0) return;

        startPositions.Clear();
        topPositions.Clear();

        // Bắt đầu từ gốc 0,0 và dàn sang hai bên
        float currentX = 0f;

        for (int i = 0; i < spikes.Count; i++)
        {
            Transform spike = spikes[i];

            // Scale xen kẽ -1, 1, -1, 1,...
            Vector3 scale = spike.localScale;
            scale.x = (i % 2 == 0) ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            spike.localScale = scale;

            // Đặt vị trí
            if (i == 0)
            {
                currentX = 0f;
            }
            else
            {
                // Lấy cọc trước để xác định khoảng cách đúng
                Transform prev = spikes[i - 1];
                float prevScaleX = prev.localScale.x;
                // Chọn khoảng cách dựa theo hướng xen kẽ
                if (prevScaleX > 0)
                    currentX += spacingPosToNeg;
                else
                    currentX += spacingNegToPos;
            }

            spike.localPosition = new Vector3(currentX, yPos, 0f);

            // Random rotation * localscale.x
            float rawRot = Random.Range(rotMin, rotMax);
            float finalRot = rawRot * Mathf.Sign(scale.x);
            spike.localRotation = Quaternion.Euler(0, 0, finalRot);

            startPositions.Add(spike.localPosition);

            // Tính hướng chéo di chuyển
            Vector3 dir = Quaternion.Euler(0, 0, finalRot) * Vector3.up;
            float moveTime = Random.Range(moveTimeMin, moveTimeMax);
            float distance = speed * moveTime;

            Vector3 topPos = spike.localPosition + dir * distance;
            topPositions.Add(topPos);
        }

        // Căn giữa lại toàn bộ để dàn đều quanh (0,0)
        float midOffset = (spikes[0].localPosition.x + spikes[spikes.Count - 1].localPosition.x) / 2f;
        for (int i = 0; i < spikes.Count; i++)
        {
            Transform s = spikes[i];
            Vector3 pos = s.localPosition;
            pos.x -= midOffset;
            s.localPosition = pos;
            startPositions[i] = pos;

            // cập nhật lại topPositions theo offset mới
            Vector3 dir = Quaternion.Euler(0, 0, s.localEulerAngles.z) * Vector3.up;
            float moveTime = Random.Range(moveTimeMin, moveTimeMax);
            float distance = speed * moveTime;
            topPositions[i] = pos + dir * distance;
        }
    }

    IEnumerator WaveOnceRoutine()
    {
        int currentIndex = spikes.Count - 1;
        Queue<int> activeIndices = new Queue<int>();

        while (currentIndex >= 0)
        {
            int idx = currentIndex;
            Transform spike = spikes[idx];
            Vector3 start = startPositions[idx];
            Vector3 end = topPositions[idx];
            float moveTime = Random.Range(moveTimeMin, moveTimeMax);

            // Đi lên
            StartCoroutine(MoveSpike(spike, start, end, moveTime));

            activeIndices.Enqueue(idx);

            // Nếu vượt quá số lượng cho phép → cái lâu nhất đi xuống
            if (activeIndices.Count > nActive)
            {
                int oldIdx = activeIndices.Dequeue();
                Transform oldSpike = spikes[oldIdx];
                float oldMoveTime = Random.Range(moveTimeMin, moveTimeMax);
                StartCoroutine(MoveSpike(oldSpike, topPositions[oldIdx], startPositions[oldIdx], oldMoveTime));
            }

            currentIndex--;
            yield return new WaitForSeconds(delayBetween);
        }

        // Sau khi tất cả đã đi lên, chờ rồi cho n cái cuối cùng hạ xuống
        yield return new WaitForSeconds(delayBetween);
        while (activeIndices.Count > 0)
        {
            int idx = activeIndices.Dequeue();
            Transform spike = spikes[idx];
            float moveTime = Random.Range(moveTimeMin, moveTimeMax);
            StartCoroutine(MoveSpike(spike, topPositions[idx], startPositions[idx], moveTime));
            yield return new WaitForSeconds(delayBetween);
        }
    }

    IEnumerator MoveSpike(Transform spike, Vector3 from, Vector3 to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float lerp = Mathf.Clamp01(t / duration);
            spike.localPosition = Vector3.Lerp(from, to, lerp);
            yield return null;
        }
        spike.localPosition = to;
    }
}
