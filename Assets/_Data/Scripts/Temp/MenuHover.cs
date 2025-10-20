using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuHover : MyMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private TextMeshProUGUI text;

	[Header("Hover Colors")]
	[SerializeField] private Color normalColor = new Color32(176, 176, 176, 255); // #B0B0B0 xám sáng
	[SerializeField] private Color hoverColor = new Color32(255, 211, 105, 255); // #FFD369 vàng

	protected override void LoadComponents()
	{
		base.LoadComponents();
		if (this.text == null)
		{
			this.text = GetComponent<TextMeshProUGUI>();
			if (this.text == null)
				this.text = GetComponentInChildren<TextMeshProUGUI>();
		}
	}

	protected override void Start()
	{
		base.Start();
		if (this.text != null) this.text.faceColor = this.normalColor;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.text != null) this.text.faceColor = this.hoverColor;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.text != null) this.text.faceColor = this.normalColor;
	}
}
