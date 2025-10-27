// MenuHoverSmooth.cs
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuHoverSmooth : MyMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] protected TextMeshProUGUI text;
	public TextMeshProUGUI Text => text;
	public Color normalColor = Color.white;
	public Color hoverColor = Color.yellow;
	public float duration = 0.12f;

	private Coroutine running;

	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadTextMesh();
	}

    protected override void Start()
    {
        base.Start();
        if (text != null) text.color = normalColor;
    }

	protected virtual void LoadTextMesh()
	{
		if (text != null) return;
		text = GetComponentInChildren<TextMeshProUGUI>();
		Debug.LogWarning(transform.name + ": LoadTextMesh", gameObject);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		StartColorTween(hoverColor);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		StartColorTween(normalColor);
	}

	void StartColorTween(Color target)
	{
		if (running != null) StopCoroutine(running);
		running = StartCoroutine(ColorTween(text.color, target));
	}

	IEnumerator ColorTween(Color from, Color to)
	{
		float t = 0f;
		while (t < duration)
		{
			t += Time.unscaledDeltaTime; // UI should ignore timescale
			if (text != null) text.color = Color.Lerp(from, to, t / duration);
			yield return null;
		}
		if (text != null) text.color = to;
		running = null;
	}
}

