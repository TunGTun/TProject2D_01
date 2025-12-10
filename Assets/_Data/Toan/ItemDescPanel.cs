using TMPro;
using UnityEngine;

public class ItemDescPanel : MyMonoBehaviour
{
	// Singleton pattern
	public static ItemDescPanel Instance { get; private set; }

	[Header("UI References")]
	[SerializeField] protected TextMeshProUGUI descriptionText;
	[SerializeField] protected GameObject panelObj;

	protected override void Awake()
	{
		base.Awake();
		if (ItemDescPanel.Instance != null && ItemDescPanel.Instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		ItemDescPanel.Instance = this;
	}

	protected override void Start()
	{
		base.Start();
		this.HideDesc();
	}

	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadDescriptionText();
		this.LoadPanelObj();
	}

	// --- Load Components (Unpackagely) ---

	protected virtual void LoadDescriptionText()
	{
		if (this.descriptionText != null) return;

		// Tự động tìm object con có tên là "DescriptionText" hoặc "Text (TMP)"
		// true nghĩa là tìm kể cả khi nó đang bị tắt (Inactive)
		this.descriptionText = GetComponentInChildren<TextMeshProUGUI>(true);

		// Nếu muốn tìm chính xác theo tên để tránh nhầm với ShopTitle:
		Transform textTr = transform.Find("DescriptionText");
		if (textTr == null) textTr = transform.Find("Text (TMP)"); // Fallback tên cũ

		if (textTr != null)
		{
			this.descriptionText = textTr.GetComponent<TextMeshProUGUI>();
		}

		Debug.Log(transform.name + ": LoadDescriptionText " + (this.descriptionText != null ? "OK" : "NULL"), gameObject);
	}

	protected virtual void LoadPanelObj()
	{
		if (this.panelObj != null) return;
		this.panelObj = this.gameObject;
	}

	// --- Public Functions ---

	public void ShowDesc(string content)
	{
		// 1. Bật Panel lên trước
		if (this.panelObj != null)
		{
			this.panelObj.SetActive(true);
		}

		// 2. Set text
		if (this.descriptionText != null)
		{
			this.descriptionText.text = content;
		}
		else
		{
			// Fallback tìm lại nếu null
			this.LoadDescriptionText();
			if (this.descriptionText != null) this.descriptionText.text = content;
		}
	}

	public void HideDesc()
	{
		if (this.panelObj != null)
		{
			this.panelObj.SetActive(false);
		}
	}
}