using System.Collections;
using UnityEngine;

public class PauseMainMenuButton : ABaseButton
{
    //[SerializeField] protected PausePanelCtrl pausePanelCtrl;
    //public PausePanelCtrl PausePanelCtrl => pausePanelCtrl;

    [SerializeField] protected DontDestroyOnLoad dontDestroyOnLoad;
    public DontDestroyOnLoad DontDestroy => dontDestroyOnLoad;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        //this.LoadPausePanelCtrl();
        this.LoadDontDestroy();
    }

    //protected virtual void LoadPausePanelCtrl()
    //{
    //    if (this.pausePanelCtrl != null) return;
    //    this.pausePanelCtrl = GetComponentInParent<PausePanelCtrl>();
    //    Debug.Log(transform.name + ": LoadPausePanelCtrl", gameObject);
    //}

    protected virtual void LoadDontDestroy()
    {
        if (this.dontDestroyOnLoad != null) return;
        this.dontDestroyOnLoad = GetComponentInParent<DontDestroyOnLoad>();
        Debug.Log(transform.name + ": LoadDontDestroy", gameObject);
    }

    protected override void OnClick()
    {
        this.MainMenu();
    }

    protected virtual void MainMenu()
    {
        StartCoroutine(MainMenuRoutine());
        MySceneManager.Instance.LoadScene(EScene.MainMenu.ToString());
    }

    protected virtual IEnumerator MainMenuRoutine()
    {
        MySceneManager.Instance.SceneTransitionAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        yield return new WaitForSecondsRealtime(SSceneTransitionData.AnimationDuration);

        if (SaveLoadSceneData.Instance.Boss == null || (SaveLoadSceneData.Instance.Boss != null && SaveLoadSceneData.Instance.SceneData.BossDefeated))
        {
            SaveLoadManager.Instance.SavePlayer();
        }

        Time.timeScale = 1;

        MySceneManager.Instance.SceneTransitionAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        InputManager.Instance.SetCanControl(true);
        Destroy(this.dontDestroyOnLoad.gameObject);
    }
}