using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TPlatformVerticalController : MonoBehaviour
{
	[Header("Movement Settings")]
	public float moveSpeed = 2f;
	public float travelDistance = 6f;

	private Rigidbody2D rb;
	private Vector3 startPos;
	private bool movingDown = false;
	private bool isMoving = false;

	private Rigidbody2D standingPlayerRb = null;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.bodyType = RigidbodyType2D.Kinematic;  // QUAN TRỌNG
		rb.gravityScale = 0;
		rb.freezeRotation = true;
		rb.interpolation = RigidbodyInterpolation2D.Interpolate;

		startPos = transform.position;
	}

	void Update()
	{
		if (InputManager.Instance.InteractInput)
		{
			isMoving = true;
			movingDown = transform.position.y >= startPos.y - 0.01f;
		}
	}

	void FixedUpdate()
	{
		if (isMoving)
			MovePlatform();
	}

	void MovePlatform()
	{
		Vector3 current = transform.position;
		Vector3 next = current;

		float bottom = startPos.y - travelDistance;
		float top = startPos.y;

		if (movingDown)
		{
			next.y -= moveSpeed * Time.fixedDeltaTime;

			if (next.y <= bottom)
			{
				next.y = bottom;
				isMoving = false;
			}
		}
		else
		{
			next.y += moveSpeed * Time.fixedDeltaTime;

			if (next.y >= top)
			{
				next.y = top;
				isMoving = false;
			}
		}

		Vector3 delta = next - current;
		rb.MovePosition(next);

		if (standingPlayerRb != null)
		{
			standingPlayerRb.MovePosition(standingPlayerRb.position + (Vector2)delta);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (!col.transform.CompareTag("Player")) return;

		foreach (var cp in col.contacts)
		{
			if (cp.normal.y > 0.5f)
			{
				standingPlayerRb = col.transform.GetComponent<Rigidbody2D>();
				break;
			}
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (col.transform.GetComponent<Rigidbody2D>() == standingPlayerRb)
			standingPlayerRb = null;
	}
}
