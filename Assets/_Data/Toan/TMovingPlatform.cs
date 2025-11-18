using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TPlatformController : MyMonoBehaviour
{
	// Quy tắc: [Header()] và [SerializeField] cho biến hiện trong editor, không có "_"
	[Header("Movement Settings")]
	[SerializeField] private float moveSpeed = 2f;      // Tốc độ di chuyển (nên là 2f cho cả 3 vật thể)
	[SerializeField] private float travelDistance = 6f;   // Quãng đường di chuyển tối đa từ tâm (12 ô tổng cộng)
	[SerializeField] private bool moveRightInitially = true; // Vật thể này có di chuyển sang phải trước không?

	// Quy tắc: Biến không hiện trong editor (private/protected) có "_" ở đầu, chữ đầu viết thường
	protected Rigidbody2D _rigidbody2D;
	protected Vector3 _startPos;
	protected bool _movingRight; // Hướng di chuyển hiện tại

	// Quy tắc: Hàm viết hoa chữ cái đầu tất cả
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

		// Quy tắc: Sử dụng this.
		this._rigidbody2D = this.GetComponent<Rigidbody2D>();
		this._rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
		this._rigidbody2D.freezeRotation = true;
		Debug.LogWarning(this.transform.name + " LoadRigidbody2D: Rigidbody2D loaded successfully.", this.gameObject);
	}

	// Unity Lifecycle: Dùng FixedUpdate cho vật lý
	protected void FixedUpdate()
	{
		this.MovePlatform();
	}

	protected virtual void MovePlatform()
	{
		// Giới hạn Trái/Phải
		float leftLimit = this._startPos.x - this.travelDistance;
		float rightLimit = this._startPos.x + this.travelDistance;

		Vector3 pos = this.transform.position;

		if (this._movingRight)
		{
			pos.x += this.moveSpeed * Time.fixedDeltaTime;
			if (pos.x >= rightLimit)
			{
				pos.x = rightLimit;
				this._movingRight = false; // Đổi hướng sang Trái
			}
		}
		else // Di chuyển sang Trái
		{
			pos.x -= this.moveSpeed * Time.fixedDeltaTime;
			if (pos.x <= leftLimit)
			{
				pos.x = leftLimit;
				this._movingRight = true; // Đổi hướng sang Phải
			}
		}

		// Di chuyển Platform
		this._rigidbody2D.MovePosition(pos);
	}
}