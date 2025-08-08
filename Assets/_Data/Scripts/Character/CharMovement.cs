using UnityEngine;
public class CharMovement : MyMonoBehaviour
{
    [Header("CharMovement")]
    [SerializeField] protected CharCtrl charCtrl;

    [Header("Move Settings")]
    [SerializeField] protected float _moveSpeed = 3f;
    public float xDirection;

    [Header("Jump Settings")]
    [SerializeField] protected float jumpForce = 6f;
    [SerializeField] protected float coyoteTime = 0.1f; //Jump sau khi roi khoi dat
    [SerializeField] protected float jumpBufferTime = 0.1f; //bam Jump trc khi cham dat
    protected int jumpCount = 0;
    protected int maxJump = 1;
    protected float coyoteTimeCounter;
    protected float jumpBufferCounter;

    [Header("Dash Settings")]
    [SerializeField] protected float dashSpeed = 7f;
    [SerializeField] protected float dashDuration = 0.3f;
    [SerializeField] protected float dashCooldown = 0.5f;
    protected bool isDashing = false;
    protected float dashTimeCounter;
    protected float dashCooldownCounter;
    protected float dashDirection = 1f;

    [Header("Gravity Settings")]
    [SerializeField] private float baseGravityScale = 1f;
    [SerializeField] private float maxGravityScale = 30f;
    [SerializeField] private float gravityIncreaseRate = 3f;
    private float currentGravityScale;
    //AUTO LOAD
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharCtrl();
    }

    protected virtual void LoadCharCtrl()
    {
        if (charCtrl != null) return;
        charCtrl = GetComponentInParent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    private void Update()
    {
        this.GetXDirection();

        // Coyote Time
        if (charCtrl.CharState.IsGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        // Jump Buffer
        if (InputManager.Instance.JumpInput)
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        // DASH INPUT
        if (InputManager.Instance.DashInput && dashCooldownCounter <= 0f && !isDashing)
        {
            StartDash();
        }

        // Đang trong thời gian dash
        if (isDashing)
        {
            dashTimeCounter -= Time.deltaTime;
            if (dashTimeCounter <= 0f)
            {
                EndDash();
            }
        }

        dashCooldownCounter -= Time.deltaTime;

        this.HandleJump();
        this.ResetJumpCount();
    }


    private void FixedUpdate()
    {
        this.HandleGravityScaling();
        if (isDashing)
        {
            PerformDash();
        }
        else
        {
            this.Move();
        }
    }

    protected virtual void GetXDirection()
    {
        xDirection = InputManager.Instance.MoveInput;
    }
    protected virtual void Move()
    {
        float _moveStep = xDirection * _moveSpeed;
        charCtrl.RigidBody2D.linearVelocity = new Vector2(_moveStep, charCtrl.RigidBody2D.linearVelocity.y);

        this.RunningFlip();
    }

    protected virtual void RunningFlip()
    {
        if (xDirection != 0)
        {
            Vector3 scale = charCtrl.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (xDirection < 0 ? -1 : 1);
            charCtrl.transform.localScale = scale;
        }
    }

    public float GetMoveSpeed()
    {
        return _moveSpeed;
    }
    public void SetMoveSpeed(float newSpeed)
    {
        _moveSpeed = newSpeed;
    }

    //CHARACTER JUMP

    protected virtual void ResetJumpCount()
    {
        if (!charCtrl.CharState.IsGrounded) return;
        jumpCount = 0;
    }
    protected virtual void HandleJump()
    {
        // Nếu có jump input và số lần nhảy chưa vượt quá giới hạn
        if (jumpBufferCounter > 0f && jumpCount < maxJump)
        {
            // Cho phép nhảy nếu: đang đứng đất HOẶC còn trong coyoteTime
            if (charCtrl.CharState.IsGrounded || coyoteTimeCounter > 0f)
            {
                charCtrl.RigidBody2D.linearVelocity = new Vector2(
                    charCtrl.RigidBody2D.linearVelocity.x,
                    jumpForce
                );

                jumpCount++;
                jumpBufferCounter = 0f; // reset buffer sau khi nhảy
            }
            // Nếu đang ở trên không nhưng vẫn còn lượt nhảy (double-jump)
            else if (jumpCount < maxJump)
            {
                charCtrl.RigidBody2D.linearVelocity = new Vector2(
                    charCtrl.RigidBody2D.linearVelocity.x,
                    jumpForce
                );

                jumpCount++;
                jumpBufferCounter = 0f;
            }
        }
    }

    //CHARACTER DASH
    protected virtual void StartDash()
    {
        isDashing = true;
        dashTimeCounter = dashDuration;
        dashCooldownCounter = dashCooldown;

        // Xác định hướng dash
        if (xDirection != 0)
            dashDirection = Mathf.Sign(xDirection);
        else
            dashDirection = Mathf.Sign(charCtrl.transform.localScale.x); // nếu đứng yên, dash theo hướng đang facing

        // Nếu vẫn không xác định được hướng (scale.x = 0), dùng hướng mặc định bên phải
        if (dashDirection == 0)
            dashDirection = 1f;
    }

    protected virtual void EndDash()
    {
        isDashing = false;
    }

    protected virtual void PerformDash()
    {
        //    charCtrl.RigidBody2D.linearVelocity = new Vector2(dashDirection * dashSpeed, 0f);
        charCtrl.RigidBody2D.linearVelocity = new Vector2(dashDirection * dashSpeed, charCtrl.RigidBody2D.linearVelocity.y);
    }

    protected virtual void HandleGravityScaling()
    {
        // Nếu đang ở trên không và đang rơi
        if (!charCtrl.CharState.IsGrounded && charCtrl.RigidBody2D.linearVelocity.y < 0 && !isDashing)
        {
            currentGravityScale += gravityIncreaseRate * Time.deltaTime;
            currentGravityScale = Mathf.Min(currentGravityScale, maxGravityScale);
        }
        else
        {
            currentGravityScale = baseGravityScale;
        }

        charCtrl.RigidBody2D.gravityScale = currentGravityScale;
    }
}
