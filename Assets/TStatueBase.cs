using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class TStatueBase : MyMonoBehaviour
{
	[Header("UI References")]
	[SerializeField] protected GameObject notificationPanel;
	[SerializeField] protected Button confirmBtn;
	[SerializeField] protected TextMeshProUGUI contentText;

	private bool _isPlayerNearby;
	private SkillLock _playerSkillLock;

	// --- Abstract Methods ---
	protected abstract void OnUnlockSpecificSkill(SkillLock skillLockScript);
	protected abstract string GetSkillName();

	// --- Override Core ---
	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadNotificationPanel();
		this.LoadConfirmButton();
		this.LoadContentText();
	}

	protected override void Start()
	{
		base.Start();
		this.SetupButtonEvent();
		this.HideNotification();
	}

	private void Update()
	{
		if (this._isPlayerNearby && this._playerSkillLock != null && InputManager.Instance.InteractInput)
		{
			this.UnlockAndShowUI();
		}
	}

	// --- Logic & UI ---
	protected virtual void UnlockAndShowUI()
	{
		this.OnUnlockSpecificSkill(this._playerSkillLock);

		if (this.contentText != null)
		{
			this.contentText.text = "Đã mở khóa: " + this.GetSkillName();
		}

		this.ShowNotification();
	}

	protected virtual void ShowNotification()
	{
		if (this.notificationPanel != null)
		{
			this.notificationPanel.SetActive(true);
			// Log đúng chuẩn: Tên Object + : + Tên Hàm
			Debug.Log(transform.name + ": ShowNotification", gameObject);
		}
	}

	protected virtual void HideNotification()
	{
		if (this.notificationPanel != null)
		{
			this.notificationPanel.SetActive(false);
			// Log đúng chuẩn
			Debug.Log(transform.name + ": HideNotification", gameObject);
		}
	}

	// --- Auto Load Components ---
	protected virtual void LoadNotificationPanel()
	{
		if (this.notificationPanel != null) return;

		GameObject foundObj = GameObject.Find("SkillPopup");
		if (foundObj != null)
		{
			this.notificationPanel = foundObj;
			Debug.Log(transform.name + ": LoadNotificationPanel", gameObject);
		}
		else
		{
			Debug.LogWarning(transform.name + ": LoadNotificationPanel (SkillPopup not found)", gameObject);
		}
	}

	protected virtual void LoadConfirmButton()
	{
		if (this.confirmBtn != null) return;

		if (this.notificationPanel != null)
		{
			this.confirmBtn = this.notificationPanel.GetComponentInChildren<Button>();
			if (this.confirmBtn != null)
			{
				Debug.Log(transform.name + ": LoadConfirmButton", gameObject);
			}
			else
			{
				Debug.LogWarning(transform.name + ": LoadConfirmButton (Button not found inside Panel)", gameObject);
			}
		}
	}

	protected virtual void LoadContentText()
	{
		if (this.contentText != null) return;

		if (this.notificationPanel != null)
		{
			this.contentText = this.notificationPanel.GetComponentInChildren<TextMeshProUGUI>();
			if (this.contentText != null)
			{
				Debug.Log(transform.name + ": LoadContentText", gameObject);
			}
		}
	}

	protected virtual void SetupButtonEvent()
	{
		if (this.confirmBtn != null)
		{
			this.confirmBtn.onClick.RemoveAllListeners();
			this.confirmBtn.onClick.AddListener(this.HideNotification);
			// Debug.Log(transform.name + ": SetupButtonEvent", gameObject); // (Bỏ comment nếu muốn log cả setup)
		}
	}

	// --- Physics ---
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			this._isPlayerNearby = true;
			this._playerSkillLock = other.GetComponent<SkillLock>();

			if (this._playerSkillLock == null)
			{
				Debug.LogWarning(transform.name + ": OnTriggerEnter2D (Player missing SkillLock script)", gameObject);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			this._isPlayerNearby = false;
			this._playerSkillLock = null;
			this.HideNotification();
		}
	}
}