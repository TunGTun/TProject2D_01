using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TPlatformController : MyMonoBehaviour
{
	[Header("Movement Settings")]
	[SerializeField] private float moveSpeed = 2f;      // Tốc độ di chuyển (nên là 2f cho cả 3 vật thể)
	[SerializeField] private float travelDistance = 6f;   // Quãng đường di chuyển tối đa từ tâm (12 ô tổng cộng)
	[SerializeField] private bool moveRightInitially = true; // Vật thể này có di chuyển sang phải trước không?

	protected Rigidbody2D _rigidbody2D;
	protected Vector3 _startPos;
	protected bool _movingRight;

    private Vector2 currentVelocity;

	protected bool isPlayerOnPlatform = false;

    protected override void Awake()
	{
		base.Awake();
		this._startPos = this.transform.position; // Lưu vị trí ban đầu
		this._movingRight = this.moveRightInitially; // Thiết lập hướng di chuyển ban đầu
	}

	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadRigidbody2D();
	}

	protected virtual void LoadRigidbody2D()
	{
		if (this._rigidbody2D != null) return;

		this._rigidbody2D = this.GetComponent<Rigidbody2D>();
		this._rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
		this._rigidbody2D.freezeRotation = true;
		Debug.LogWarning(this.transform.name + " LoadRigidbody2D: Rigidbody2D loaded successfully.", this.gameObject);
	}

	protected void FixedUpdate()
	{
		this.MovePlatform();
	}

	protected virtual void MovePlatform()
	{
        // tính velocity trước
        float direction = _movingRight ? 1f : -1f;
        currentVelocity = new Vector2(direction * moveSpeed, 0f);

        // limits
        float leftLimit = _startPos.x - travelDistance;
        float rightLimit = _startPos.x + travelDistance;

        Vector3 pos = transform.position;

        // move theo hướng
        pos.x += currentVelocity.x * Time.fixedDeltaTime;

        // check giới hạn
        if (_movingRight && pos.x >= rightLimit)
        {
            pos.x = rightLimit;
            _movingRight = false;
        }
        else if (!_movingRight && pos.x <= leftLimit)
        {
            pos.x = leftLimit;
            _movingRight = true;
        }

        // apply
        _rigidbody2D.MovePosition(pos);
		this.SetCharVelocity();
    }

	protected virtual void SetCharVelocity()
	{
        if (!isPlayerOnPlatform) return;
        CharStateCtrl charStateCtrl = CharCtrl.Instance.CharStateCtrl;
        if (charStateCtrl.HorizontalState.StateMachine.CurrentState == charStateCtrl.HorizontalState.idleX
			&& charStateCtrl.VerticalState.StateMachine.CurrentState == charStateCtrl.VerticalState.idleGround)
		{
			CharCtrl.Instance.RigidBody2D.linearVelocity = currentVelocity;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
        }
    }
}