using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
public class CharMovement : MyMonoBehaviour
{
    [Header("CharMovement")]
    [SerializeField] protected CharCtrl charCtrl;
    [SerializeField] protected float _moveSpeed = 3f;
    protected float _xDirection;

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

        _xDirection = InputManager.Instance.MoveInput;

       // if (_xDirection == 0) return;
    }

    protected virtual void Move()
    {
        //if (_charCtrl.CharState.GetIsDead())
        //{
        //    _charCtrl.Rigidbody2D.velocity = new Vector2(0, _charCtrl.Rigidbody2D.velocity.y);
        //    return;
        //}
        float _moveStep = _xDirection * _moveSpeed;
        //if (_charCtrl.CharState.WallJumping) return;
        //if (_charCtrl.CharState.Dashing) return;
        charCtrl.RigidBody2D.linearVelocity = new Vector2(_moveStep, charCtrl.RigidBody2D.linearVelocity.y);

        this.RunningFlip();
    }

    protected virtual void RunningFlip()
    {
        if (_xDirection != 0)
        {
            Vector3 scale = charCtrl.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (_xDirection < 0 ? -1 : 1);
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




    /// <summary>
    /// ////
    /// </summary>


    /// <summary>
    /// //////
    /// </summary>

     protected int jumpCount;
     protected bool canJump;
     protected bool finishJump;
     protected int maxExtraJump = 2;
     protected bool _finishJump;
    [SerializeField] protected float jumpForce = 6f;









    protected virtual void Jump()
    {
        if (!canJump) return;
        if (!charCtrl.CharState.IsGrounded) return;
        this.charCtrl.RigidBody2D.linearVelocity = new Vector2(charCtrl.RigidBody2D.linearVelocity.x, this.jumpForce);
        canJump = !canJump;
        jumpCount++;
    }

    protected virtual void CheckJump()
    {
        if (InputManager.Instance.JumpInput && jumpCount < maxExtraJump) this.canJump = true;
    }

    protected virtual void ResetJumpCount()
    {
        if (charCtrl.RigidBody2D.linearVelocity.y > 0.1) return;
        if (charCtrl.RigidBody2D.linearVelocity.y < -0.1) return;
        if (!charCtrl.CharState.IsGrounded) return;
        if (jumpCount == 0) return;
        jumpCount = 0;
    }


}
