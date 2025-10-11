using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MySingleton<MySceneManager>
{
    [SerializeField] protected EScene lastScene;
    public EScene LastScene { get => lastScene; set => lastScene = value; }


    public virtual void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
