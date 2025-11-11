using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MySingleton<MySceneManager>
{
    [SerializeField] protected EScene lastScene;
    public EScene LastScene { get => lastScene; set => lastScene = value; }

    [SerializeField] protected Animator sceneTransitionAnimator;
    public Animator SceneTransitionAnimator => sceneTransitionAnimator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSceneTransitionAnimator();
    }

    protected virtual void LoadSceneTransitionAnimator()
    {
        if (sceneTransitionAnimator != null) return;
        this.sceneTransitionAnimator = GetComponentInChildren<Animator>();
        Debug.Log(transform.name + ": LoadSceneTransitionAnimator", gameObject);
    }
    
    public virtual void LoadScene(string sceneName)
    {
        StartCoroutine(SceneTransitionRoutine(sceneName));
    }

    protected virtual IEnumerator SceneTransitionRoutine(string sceneName)
    {
        this.sceneTransitionAnimator.SetTrigger(SSceneTransitionData.ANIMATION_END_TRIGGER);
        if (Time.timeScale == 0)
            yield return new WaitForSecondsRealtime(SSceneTransitionData.AnimationDuration);
        else
            yield return new WaitForSeconds(SSceneTransitionData.AnimationDuration);
        SceneManager.LoadSceneAsync(sceneName);
        this.sceneTransitionAnimator.SetTrigger(SSceneTransitionData.ANIMATION_START_TRIGGER);
    }

    public virtual string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    //public virtual EScene GetCurrentSceneEnum()
    //{
    //    string currentName = SceneManager.GetActiveScene().name;
    //    if (System.Enum.TryParse(currentName, out EScene sceneEnum))
    //        return sceneEnum;

    //    Debug.LogWarning("Scene name not found in EScene enum: " + currentName);
    //    return EScene.None; // ho?c giá tr? m?c ??nh b?n mu?n
    //}
}
