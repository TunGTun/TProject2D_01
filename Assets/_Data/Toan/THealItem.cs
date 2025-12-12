using UnityEngine;

public class THealItem : ShopItemBase
{
	public override void OnBuy()
	{
		if (this._isPurchased)
		{
			return;
		}

        CharCtrl.Instance.CharData.AddMaxHP(1);

        CharCtrl.Instance.CharData.AddHP(1);

		this.MarkAsPurchased();
	}
}