using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	[Tooltip("Bạn có thể gọi LoadScene từ Button và truyền tên scene này")]
	public string newGameSceneName = "GameScene";

	// Load scene theo tên truyền vào
	public void LoadSceneByName(string sceneName)
	{
		if (string.IsNullOrEmpty(sceneName))
		{
			Debug.LogWarning("Tên scene rỗng!");
			return;
		}
		SceneManager.LoadScene(sceneName); // load ngay lập tức scene theo tên
	}

	// Load scene đã đặt sẵn trong inspector (newGameSceneName)
	public void LoadNewGame()
	{
		if (string.IsNullOrEmpty(newGameSceneName))
		{
			Debug.LogWarning("newGameSceneName chưa đặt trong MenuManager!");
			return;
		}
		SceneManager.LoadScene(newGameSceneName);
	}

	// Quit game (Application.Quit chạy khi build; trong Editor dùng EditorApplication)
	public void QuitGame()
	{
		Application.Quit();
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}
}
