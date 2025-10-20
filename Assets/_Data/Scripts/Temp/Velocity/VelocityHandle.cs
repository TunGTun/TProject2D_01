using UnityEngine;

[System.Serializable]
public class VelocityHandle
{
    private Rigidbody2D rb;

    private bool hasSetX = false;
    private bool hasSetY = false;

    private float targetVelX;
    private float targetVelY;

    private int priorityX = 0;
    private int priorityY = 0;

    public VelocityHandle(Rigidbody2D rb)
    {
        this.rb = rb;
    }

    public void RequestX(float velX, int priority = 0)
    {
        if (!hasSetX || priority >= priorityX)
        {
            targetVelX = velX;
            priorityX = priority;
            hasSetX = true;
        }
    }

    public void RequestY(float velY, int priority = 0)
    {
        if (!hasSetY || priority >= priorityY)
        {
            targetVelY = velY;
            priorityY = priority;
            hasSetY = true;
        }
    }

    public void Request(float velX, float velY, int priority = 0)
    {
        RequestX(velX, priority);
        RequestY(velY, priority);
    }

    public void Apply()
    {
        if (!hasSetX && !hasSetY) return;

        Vector2 current = rb.linearVelocity;

        float newX = hasSetX ? targetVelX : current.x;
        float newY = hasSetY ? targetVelY : current.y;

        if (!Mathf.Approximately(newX, current.x) || !Mathf.Approximately(newY, current.y))
            rb.linearVelocity = new Vector2(newX, newY);

        Reset();
    }

    private void Reset()
    {
        hasSetX = false;
        hasSetY = false;
        priorityX = 0;
        priorityY = 0;
    }
}