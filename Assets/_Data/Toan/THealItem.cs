using UnityEngine;
using UnityEngine.UI;

public class THealItem : MyMonoBehaviour
{
	[Header("Heal Item")]
	[SerializeField] private Button buyButton;
	[SerializeField] private Image itemIcon;
	[SerializeField] private Color usedColor = new Color(0.5f, 0.5f, 0.5f);

	private bool _used = false;

	// LƯU Ý: lấy CharCtrl → rồi lấy CharData trong đó
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

	// Tìm CharCtrl của Player
	protected virtual void LoadCharCtrl()
	{
		if (this._charCtrl != null) return;

		this._charCtrl = FindObjectOfType<CharCtrl>();

		if (this._charCtrl == null)
			Debug.LogError(this.transform.name + ": CharCtrl NOT FOUND!", this.gameObject);
		else
			Debug.Log("Found CharCtrl: " + this._charCtrl.name);
	}

	// Lấy CharData từ CharCtrl (đây mới là máu thật)
	protected virtual void LoadCharData()
	{
		if (this._charData != null) return;
		if (this._charCtrl == null) return;

		this._charData = this._charCtrl.CharData;

		if (this._charData == null)
			Debug.LogError(this.transform.name + ": CharData NOT FOUND IN CharCtrl!", this.gameObject);
		else
			Debug.Log("Current HP: " + this._charData.CurrentHP);
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

		// 🔥 HP thật của nhân vật 
		int hp = this._charData.CurrentHP;
		int maxHp = this._charData.MaxHP;

		Debug.Log("Before Heal → HP: " + hp + "/" + maxHp);
		this._charData.AddMaxHP(1);
		this._charData.AddHP(1);
		this.SetUsed();
		Debug.Log("After Heal → HP: " + this._charData.CurrentHP + "/" + this._charData.MaxHP);
	}

	protected virtual void SetUsed()
	{
		this._used = true;

		if (this.itemIcon != null)
			this.itemIcon.color = this.usedColor;

		if (this.buyButton != null)
			this.buyButton.interactable = false;

		Debug.LogWarning(this.transform.name + ": ItemUsed", this.gameObject);
	}
}
