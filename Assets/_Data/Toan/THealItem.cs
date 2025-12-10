using UnityEngine;

public class THealItem : ShopItemBase
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
			Debug.Log("Item HP này đã mua rồi!");
			return;
		}

		if (this._charData == null) return;

		// 2. Logic cũ của bạn: Tăng 1 Max HP và 1 HP hiện tại
		Debug.Log("Before Heal -> HP: " + this._charData.CurrentHP + "/" + this._charData.MaxHP);

		this._charData.AddMaxHP(1);
		this._charData.AddHP(1);

		Debug.Log("After Heal -> HP: " + this._charData.CurrentHP + "/" + this._charData.MaxHP);

		// 3. Đánh dấu đã mua
		this.MarkAsPurchased();
	}
}