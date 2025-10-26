using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameButton : ABaseButton
{
    protected override void OnClick()
    {
        this.NewGame();
    }

    protected virtual void NewGame()
    {
        SaveLoadManager.Instance.DeleteSaveData();
        MySceneManager.Instance.LoadScene(EScene.IntermediaryScene.ToString());
    }
}
