using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TMovingPlatform : MyMonoBehaviour
{
	[Header("Movement Settings")]
	[SerializeField] protected float moveSpeed = 2f;       // tốc độ di chuyển
	[SerializeField] protected float tileDistance = 1f;    // đúng 1 tile

	protected Rigidbody2D _rigidbody2D;
	protected Vector3 _startPos;
	protected bool _movingRight = true;

	protected override void Awake()
	{
		base.Awake();
		this._startPos = this.transform.position; // lưu vị trí ban đầu
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
		Debug.LogWarning(transform.name + "LoadRigidbody2D: ", gameObject);
	}

	protected void FixedUpdate()
	{
		this.MovePlatform();
	}

	protected virtual void MovePlatform()
	{
		float leftLimit = this._startPos.x - this.tileDistance;
		float rightLimit = this._startPos.x + this.tileDistance;

		Vector3 pos = this.transform.position;

		if (this._movingRight)
		{
			pos.x += this.moveSpeed * Time.fixedDeltaTime;
			if (pos.x >= rightLimit)
			{
				pos.x = rightLimit;
				this._movingRight = false;
			}
		}
		else
		{
			pos.x -= this.moveSpeed * Time.fixedDeltaTime;
			if (pos.x <= leftLimit)
			{
				pos.x = leftLimit;
				this._movingRight = true;
			}
		}

		this._rigidbody2D.MovePosition(pos);
	}
}
