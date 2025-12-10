using UnityEngine;
using UnityEngine.UI;

public class ShopBuyButton : MyMonoBehaviour
{
	[SerializeField] protected Button btnBuy;

	protected override void LoadComponents()
	{
		base.LoadComponents();
		if (this.btnBuy == null) this.btnBuy = GetComponent<Button>();
	}

	protected override void Start()
	{
		base.Start();
		if (this.btnBuy != null)
		{
			this.btnBuy.onClick.AddListener(this.OnClickBuy);
		}
	}

	private void OnClickBuy()
	{
		// Gọi thẳng lên Giám đốc ShopManager
		if (ShopManager.Instance != null)
		{
			ShopManager.Instance.BuySelectedItem();
		}
	}
}