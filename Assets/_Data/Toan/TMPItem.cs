using UnityEngine;
using UnityEngine.UI;

public class TMPItem : MyMonoBehaviour
{
	[Header("MP Item")]
	[SerializeField] private Button buyButton;                 // Nút mua item
	[SerializeField] private Image itemIcon;                   // Icon item
	[SerializeField] private Color usedColor = new Color(0.5f, 0.5f, 0.5f);

	private bool _used = false;

	// Dùng đúng hệ thống hiện tại: lấy CharCtrl -> rồi lấy CharData
	private CharCtrl _charCtrl;
	private CharData _charData;

	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadBuyButton();
		this.LoadItemIcon();
		this.LoadCharCtrl();
		this.LoadCharData();
	}

	protected virtual void LoadBuyButton()
	{
		if (this.buyButton != null) return;

		this.buyButton = this.GetComponent<Button>();
		Debug.LogWarning(this.transform.name + ": LoadBuyButton", this.gameObject);
	}

	protected virtual void LoadItemIcon()
	{
		if (this.itemIcon != null) return;

		this.itemIcon = this.GetComponentInChildren<Image>();
		Debug.LogWarning(this.transform.name + ": LoadItemIcon", this.gameObject);
	}

	protected virtual void LoadCharCtrl()
	{
		if (this._charCtrl != null) return;

		this._charCtrl = FindObjectOfType<CharCtrl>();

		if (this._charCtrl == null)
			Debug.LogError(this.transform.name + ": CharCtrl NOT FOUND!", this.gameObject);
		else
			Debug.Log("Found CharCtrl: " + this._charCtrl.name);
	}

	protected virtual void LoadCharData()
	{
		if (this._charData != null) return;
		if (this._charCtrl == null) return;

		this._charData = this._charCtrl.CharData;

		if (this._charData == null)
			Debug.LogError(this.transform.name + ": CharData NOT FOUND IN CharCtrl!", this.gameObject);
		else
			Debug.Log("Current MP: " + this._charData.CurrentMP);
	}

	protected override void Start()
	{
		base.Start();

		if (this.buyButton != null)
			this.buyButton.onClick.AddListener(this.OnBuyClick);
	}

	protected virtual void OnBuyClick()
	{
		if (this._used) return;

		if (this._charData == null)
		{
			Debug.LogError("CharData missing!", this.gameObject);
			return;
		}

		int mp = this._charData.CurrentMP;
		int maxMp = this._charData.MaxMP;

		Debug.Log("Before MP Item → MP: " + mp + "/" + maxMp);
		this._charData.AddMaxMP(100);
		this._charData.AddMP(100);
		this.SetUsed();
		Debug.Log("After MP Item → MP: " + this._charData.CurrentMP + "/" + this._charData.MaxMP);
	}

	// Khi item đã dùng
	protected virtual void SetUsed()
	{
		this._used = true;

		if (this.itemIcon != null)
			this.itemIcon.color = this.usedColor;

		if (this.buyButton != null)
			this.buyButton.interactable = false;

		Debug.LogWarning(this.transform.name + ": MPItemUsed", this.gameObject);
	}
}
