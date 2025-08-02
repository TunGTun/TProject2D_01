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
    protected bool canJump;
    protected int maxExtraJump = 2;
    [SerializeField] protected float jumpForce = 6f;
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
        this.CheckJump();
        this.ResetJumpCount();
    }
    private void FixedUpdate()
    {
        this.Move();
        this.Jump();
    }
    protected virtual void GetXDirection()
    {

        xDirection = InputManager.Instance.MoveInput;
       // if (_xDirection == 0) return;
    }
    //CHARACTER MOVE
    protected virtual void Move()
    {
        //if (_charCtrl.CharState.GetIsDead())
        //{
        //    _charCtrl.Rigidbody2D.velocity = new Vector2(0, _charCtrl.Rigidbody2D.velocity.y);
        //    return;
        //}
        float _moveStep = xDirection * _moveSpeed;
        //if (_charCtrl.CharState.WallJumping) return;
        //if (_charCtrl.CharState.Dashing) return;
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
    protected virtual void Jump()
    {
        if (!canJump) return;
        this.charCtrl.RigidBody2D.linearVelocity = new Vector2(charCtrl.RigidBody2D.linearVelocity.x, this.jumpForce);
        canJump = !canJump;
        jumpCount++;
    }
    protected virtual void CheckJump()
    {
        if (!InputManager.Instance.JumpInput) return;

        if (charCtrl.CharState.IsGrounded)
        {
            jumpCount = 0;
            canJump = true;
        }

        if (canJump || jumpCount < maxExtraJump - 1)
        {
            charCtrl.RigidBody2D.linearVelocity = new Vector2(charCtrl.RigidBody2D.linearVelocity.x, jumpForce);
            jumpCount++;
            canJump = false;
        }
    }
    protected virtual void ResetJumpCount()
    {
        if (!charCtrl.CharState.IsGrounded) return;
        jumpCount = 0;
    }


}
