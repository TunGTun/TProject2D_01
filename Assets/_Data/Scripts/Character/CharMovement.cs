using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
public class CharMovement : MyMonoBehaviour
{
    [Header("CharMovement")]
    [SerializeField] protected CharCtrl charCtrl;
    [SerializeField] protected float _moveSpeed = 3f;
    protected float xDirection;
    protected int jumpCount = 0;
    protected int maxJump = 1;
    [SerializeField] protected float jumpForce = 6f;
    [SerializeField] protected float coyoteTime = 0.1f; //Jump sau khi roi khoi dat
    [SerializeField] protected float jumpBufferTime = 0.1f; //bam Jump trc khi cham dat

    protected float coyoteTimeCounter;
    protected float jumpBufferCounter;

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

        // Đếm thời gian kể từ khi rơi khỏi nền
        if (charCtrl.CharState.IsGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        // Đếm thời gian kể từ khi nhấn Jump
        if (InputManager.Instance.JumpInput)
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        this.HandleJump();
        this.ResetJumpCount();
    }

    private void FixedUpdate()
    {
        this.Move();
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
            // Nếu đang ở trên không nhưng vẫn còn lượt nhảy (ví dụ double-jump)
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

}
