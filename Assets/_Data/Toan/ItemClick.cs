using UnityEngine;

public class ItemClick : MyMonoBehaviour
{
	[Header("Item Info")]
	[TextArea]
	[SerializeField] protected string itemDescription = "Mô tả item...";

	// Dùng OnMouseDown yêu cầu Item phải có Collider 2D
	// Vì đây là UI (Canvas World), hãy chắc chắn button có BoxCollider2D
	private void OnMouseDown()
	{
		this.ShowMyDescription();
	}

	protected virtual void ShowMyDescription()
	{
		if (ItemDescPanel.Instance != null)
		{
			ItemDescPanel.Instance.ShowDesc(this.itemDescription);
		}
		else
		{
			Debug.LogWarning("ItemDescPanel Instance is NULL!");
		}
	}
}