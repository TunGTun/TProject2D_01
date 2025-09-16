using UnityEngine;

public class InputBuffer
{
    private string bufferedAction;
    private float timeBuffered;
    private readonly float bufferWindow;

    public InputBuffer(float bufferWindow = 0.2f) // default 200ms
    {
        this.bufferWindow = bufferWindow;
    }

    // Ghi input vào buffer
    public void AddInput(string action)
    {
        bufferedAction = action;
        timeBuffered = Time.time;
    }

    // Kiểm tra và consume input nếu còn hợp lệ
    public bool TryConsume(out string action)
    {
        if (!string.IsNullOrEmpty(bufferedAction) &&
            Time.time - timeBuffered <= bufferWindow)
        {
            action = bufferedAction;
            bufferedAction = null; // clear sau khi consume
            return true;
        }

        action = null;
        return false;
    }

    // Kiểm tra nhưng không consume
    public bool Peek(out string action)
    {
        if (!string.IsNullOrEmpty(bufferedAction) &&
            Time.time - timeBuffered <= bufferWindow)
        {
            action = bufferedAction;
            return true;
        }

        action = null;
        return false;
    }

    // Clear thủ công (nếu cần reset sau state change)
    public void Clear()
    {
        bufferedAction = null;
    }
}