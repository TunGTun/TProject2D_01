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
        yield return new WaitForSeconds(SSceneTransitionData.DelayEndTime);
        SceneManager.LoadSceneAsync(sceneName);
        this.sceneTransitionAnimator.SetTrigger(SSceneTransitionData.ANIMATION_START_TRIGGER);
    }
}
