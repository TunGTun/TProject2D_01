using TMPro; // Bắt buộc có để sửa chữ
using UnityEngine;
using UnityEngine.UI;

public class TStatueUnlockWithUI : MyMonoBehaviour
{
	[Header("Select Skill")]
	// 1. Cái "Trừu tượng" bạn muốn: Chọn skill ngay trên Inspector
	[SerializeField] protected ESkill skillToUnlock = ESkill.Dash;

	[Header("UI References")]
	[SerializeField] protected GameObject notificationPanel;
	[SerializeField] protected TextMeshProUGUI contentText; // Cái dòng chữ thông báo
	[SerializeField] protected Button confirmBtn;

	// Biến nội bộ
	private SkillLock _playerSkillLock;
	private bool _isPlayerNearby;

	// --- Override Core ---

	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadUI();
	}

	protected override void Start()
	{
		base.Start();
		// Ẩn bảng khi bắt đầu & Gắn nút tắt
		this.HideNotification();
		this.SetupButtonEvent();
	}

	// --- Logic Chính ---

	private void Update()
	{
		// Điều kiện: Người ở gần + Đã tìm thấy script SkillLock trên người + Bấm R
		if (this._isPlayerNearby && this._playerSkillLock != null && Input.GetKeyDown(KeyCode.R))
		{
			this.UnlockAndShowUI();
		}
	}

	protected virtual void UnlockAndShowUI()
	{
		// 1. Mở khóa skill tương ứng (Dựa trên cái bạn chọn ở Inspector)
		this._playerSkillLock.UnlockSkill(this.skillToUnlock);

		// 2. Sửa nội dung chữ trên bảng thông báo
		if (this.contentText != null)
		{
			this.contentText.text = "Đã mở khóa kỹ năng:\n" + this.skillToUnlock.ToString();
		}

		// 3. Hiện bảng lên
		this.ShowNotification();

		Debug.Log(transform.name + ": UnlockAndShowUI -> " + this.skillToUnlock, gameObject);
	}

	// --- UI Handling ---

	protected virtual void ShowNotification()
	{
		if (this.notificationPanel != null)
		{
			this.notificationPanel.SetActive(true);
		}
	}

	protected virtual void HideNotification()
	{
		if (this.notificationPanel != null)
		{
			this.notificationPanel.SetActive(false);
		}
	}

	// --- Auto Load Components (Tự tìm UI) ---

	protected virtual void LoadUI()
	{
		// 1. Tìm Panel
		if (this.notificationPanel == null)
		{
			GameObject foundObj = GameObject.Find("SkillPopup");
			if (foundObj != null)
			{
				this.notificationPanel = foundObj;
				Debug.Log(transform.name + ": LoadUI (Found Popup)", gameObject);
			}
			else
			{
				Debug.LogWarning(transform.name + ": LoadUI (Missing 'SkillPopup' object in Scene)", gameObject);
			}
		}

		// 2. Tìm Text và Button NẾU đã có Panel
		if (this.notificationPanel != null)
		{
			if (this.contentText == null)
			{
				this.contentText = this.notificationPanel.GetComponentInChildren<TextMeshProUGUI>();
			}

			if (this.confirmBtn == null)
			{
				this.confirmBtn = this.notificationPanel.GetComponentInChildren<Button>();
			}
		}
	}

	protected virtual void SetupButtonEvent()
	{
		if (this.confirmBtn != null)
		{
			this.confirmBtn.onClick.RemoveAllListeners();
			this.confirmBtn.onClick.AddListener(this.HideNotification);
		}
	}

	// --- Physics (Tìm Player) ---

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			this._isPlayerNearby = true;

			// Tìm script SkillLock trên người chơi hoặc cha của nó
			this._playerSkillLock = other.GetComponent<SkillLock>();
			if (this._playerSkillLock == null)
			{
				this._playerSkillLock = other.GetComponentInParent<SkillLock>();
			}

			if (this._playerSkillLock == null)
			{
				Debug.LogWarning(transform.name + ": OnTriggerEnter2D (Player missing SkillLock script!)", gameObject);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			this._isPlayerNearby = false;
			this._playerSkillLock = null;
			this.HideNotification(); // Đi xa tự tắt bảng
		}
	}
}