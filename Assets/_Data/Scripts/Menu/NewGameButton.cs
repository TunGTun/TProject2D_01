using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameButton : MyMonoBehaviour
{
	[SerializeField] private string sceneToLoad = "Scene_1";
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
		this.button.onClick.AddListener(() =>
		{
			SceneManager.LoadScene(sceneToLoad);
		});
	}
}
