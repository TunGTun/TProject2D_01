using UnityEngine;
using UnityEngine.UI;

public abstract class ShopItemBase : MyMonoBehaviour
{
	[Header("Base Shop Item")]
	[SerializeField] protected Button selectButton;
	[SerializeField] protected Image itemIcon;
	[SerializeField] protected Color purchasedColor = new Color(0.5f, 0.5f, 0.5f); // Màu xám

	// Biến trạng thái: Đã mua chưa? (Có "_" vì là protected)
	protected bool _isPurchased = false;

	protected override void Start()
	{
		base.Start();
		if (this.selectButton != null)
		{
			this.selectButton.onClick.AddListener(this.OnSelectItem);
		}
	}

	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadSelectButton();
		this.LoadItemIcon();
	}

	protected virtual void LoadSelectButton()
	{
		if (this.selectButton != null) return;
		this.selectButton = GetComponent<Button>();
	}

	protected virtual void LoadItemIcon()
	{
		if (this.itemIcon != null) return;
		// Tìm icon trong con (nếu script gắn ở cha)
		this.itemIcon = GetComponentInChildren<Image>();
	}

	protected virtual void OnSelectItem()
	{
		// Nếu đã mua rồi thì vẫn cho chọn (để xem) hoặc không tùy bạn
		// Ở đây mình vẫn cho chọn để nút Mua quyết định
		if (ShopManager.Instance != null)
		{
			ShopManager.Instance.SetSelectedItem(this);
		}
	}

	// Hàm dùng chung để đánh dấu "Hết Hàng"
	protected virtual void MarkAsPurchased()
	{
		this._isPurchased = true;

		if (this.itemIcon != null)
		{
			this.itemIcon.color = this.purchasedColor;
		}

		Debug.LogWarning(transform.name + ": ItemUsed (Sold Out)", gameObject);
	}

	// Các con bắt buộc phải viết hàm này
	public abstract void OnBuy();
}