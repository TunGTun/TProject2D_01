using UnityEngine;

public class TMPItem : ShopItemBase
{
	public override void OnBuy()
	{
		// 1. Kiểm tra trạng thái mua
		if (this._isPurchased)
		{
			return;
		}

		CharCtrl.Instance.CharData.AddMaxMP(100);

        CharCtrl.Instance.CharData.AddMP(100);

		this.MarkAsPurchased();
	}
}