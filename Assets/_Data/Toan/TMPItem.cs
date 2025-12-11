using UnityEngine;

public class TMPItem : ShopItemBase
{
	private CharCtrl _charCtrl;
	private CharData _charData;

	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadCharCtrl();
		this.LoadCharData();
	}

	protected virtual void LoadCharCtrl()
	{
		if (this._charCtrl != null) return;
		this._charCtrl = FindObjectOfType<CharCtrl>();
		if (this._charCtrl == null) Debug.LogError(transform.name + ": CharCtrl NOT FOUND!", gameObject);
	}

	protected virtual void LoadCharData()
	{
		if (this._charData != null) return;
		if (this._charCtrl != null) this._charData = this._charCtrl.CharData;
	}

	public override void OnBuy()
	{
		// 1. Kiểm tra trạng thái mua
		if (this._isPurchased)
		{
			Debug.Log("Item MP này đã mua rồi!");
			return;
		}

		if (this._charData == null) return;

		// 2. Logic cũ của bạn: Tăng 100 Max MP và 100 MP hiện tại
		Debug.Log("Before MP Item -> MP: " + this._charData.CurrentMP + "/" + this._charData.MaxMP);

		this._charData.AddMaxMP(100);
		this._charData.AddMP(100);

		Debug.Log("After MP Item -> MP: " + this._charData.CurrentMP + "/" + this._charData.MaxMP);

		// 3. Đánh dấu đã mua
		this.MarkAsPurchased();
	}
}