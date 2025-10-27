using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MyMonoBehaviour
{
	[SerializeField] private Button button;

	protected override void LoadComponents()
	{
		base.LoadComponents();
		if (this.button == null)
			this.button = GetComponent<Button>();
	}

	protected override void Start()
	{
		base.Start();
		this.button.onClick.AddListener(QuitGame);
	}

	private void QuitGame()
	{
		Application.Quit();
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}
}
