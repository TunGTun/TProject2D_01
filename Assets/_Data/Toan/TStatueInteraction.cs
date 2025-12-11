using UnityEngine;
using UnityEngine.UI;

public class TStatueInteraction : MyMonoBehaviour
{
	[Header("UI References")]
	[SerializeField] protected GameObject notificationPanel;
	[SerializeField] protected Button confirmBtn;

	private bool _isPlayerNearby;


	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadNotificationPanel();
		this.LoadConfirmButton();
	}

	protected override void Start()
	{
		base.Start();
		this.SetupButtonEvent();
		this.HideNotification();
	}

	private void Update()
	{
		// Nếu người chơi ở gần VÀ bấm R
		if (this._isPlayerNearby && InputManager.Instance.InteractInput)
		{
			Debug.Log("Da bam R");
			this.ShowNotification();
		}
	}

	// --- Load Components (Auto Find - Unpackagely) ---

	protected virtual void LoadNotificationPanel()
	{
		if (this.notificationPanel != null) return;

		// Tìm object tên SkillPopup trong toàn bộ Scene (vì UI nằm rời rạc trong Canvas)
		GameObject foundObj = GameObject.Find("SkillPopup");

		if (foundObj != null)
		{
			this.notificationPanel = foundObj;
			Debug.Log(transform.name + ": Đã tìm thấy SkillPopup", gameObject);
		}
		else
		{
			Debug.LogWarning(transform.name + ": Không tìm thấy 'SkillPopup'. Hãy kiểm tra tên trong Hierarchy!", gameObject);
		}
	}

	protected virtual void LoadConfirmButton()
	{
		if (this.confirmBtn != null) return;

		// Tìm nút ConfirmBtn NẰM TRONG notificationPanel
		if (this.notificationPanel != null)
		{
			// Dùng GetComponentInChildren để tìm nút con
			Button btn = this.notificationPanel.GetComponentInChildren<Button>();

			// Nếu muốn tìm chính xác tên thì dùng đoạn dưới:
			// Transform btnTransform = this.notificationPanel.transform.Find("ConfirmBtn");
			// if(btnTransform != null) btn = btnTransform.GetComponent<Button>();

			if (btn != null)
			{
				this.confirmBtn = btn;
				Debug.Log(transform.name + ": Đã tìm thấy Button trong Panel", gameObject);
			}
			else
			{
				Debug.LogWarning(transform.name + ": Panel không có Button nào!", gameObject);
			}
		}
	}

	// --- Logic Xử lý ---

	protected virtual void SetupButtonEvent()
	{
		if (this.confirmBtn != null)
		{
			// Xóa sự kiện cũ (tránh lỗi lặp)
			this.confirmBtn.onClick.RemoveAllListeners();
			// Thêm sự kiện: Bấm vào thì gọi hàm HideNotification
			this.confirmBtn.onClick.AddListener(this.HideNotification);
		}
	}

	protected virtual void ShowNotification()
	{
		if (this.notificationPanel != null)
		{
			this.notificationPanel.SetActive(true);
			Debug.Log("Hiện thông báo");
		}
	}

	protected virtual void HideNotification()
	{
		if (this.notificationPanel != null)
		{
			this.notificationPanel.SetActive(false);
			Debug.Log("Tắt thông báo");
		}
	}

	// --- Physics (Trigger) ---

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			this._isPlayerNearby = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			this._isPlayerNearby = false;
			// Đi xa tự tắt luôn cho gọn
			this.HideNotification();
		}
	}
}