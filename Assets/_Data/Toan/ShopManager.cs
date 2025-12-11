using UnityEngine;

public class ShopManager : MyMonoBehaviour
{
	// Singleton để gọi từ bất cứ đâu
	public static ShopManager Instance { get; private set; }

	// Biến chứa Item đang được chọn (Dạng Base Class)
	private ShopItemBase _currentSelectedItem;

	protected override void Awake()
	{
		base.Awake();
		if (ShopManager.Instance != null && ShopManager.Instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		ShopManager.Instance = this;
	}

	// Hàm này để các Item gọi khi bị bấm vào
	public void SetSelectedItem(ShopItemBase item)
	{
		this._currentSelectedItem = item;
		Debug.Log("ShopManager: Đã chọn item -> " + item.name);
	}

	// Hàm này để nút Mua gọi
	public void BuySelectedItem()
	{
		if (this._currentSelectedItem != null)
		{
			this._currentSelectedItem.OnBuy(); // Gọi hàm mua của item đó
		}
		else
		{
			Debug.LogWarning("Chưa chọn item nào cả!");
		}
	}
}