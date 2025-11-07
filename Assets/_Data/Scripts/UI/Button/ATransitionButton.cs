using System.Collections;
using UnityEngine;

public abstract class ATransitionButton : ABaseButton
{
    protected override void OnClick()
    {
        StartCoroutine(TransitionRoutine());
    }

    protected virtual IEnumerator TransitionRoutine()
    {
        Animator animator = MySceneManager.Instance.SceneTransitionAnimator;
        if (Time.timeScale == 0)
        {
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            animator.SetTrigger(SSceneTransitionData.ANIMATION_END_TRIGGER);
            yield return new WaitForSecondsRealtime(SSceneTransitionData.AnimationDuration);
            this.OnClickTransition();
            animator.SetTrigger(SSceneTransitionData.ANIMATION_START_TRIGGER);
            yield return new WaitForSecondsRealtime(SSceneTransitionData.AnimationDuration);
            animator.updateMode = AnimatorUpdateMode.Normal;
        }  
        else
        {
            animator.SetTrigger(SSceneTransitionData.ANIMATION_END_TRIGGER);
            yield return new WaitForSeconds(SSceneTransitionData.AnimationDuration);
            this.OnClickTransition();
            animator.SetTrigger(SSceneTransitionData.ANIMATION_START_TRIGGER);
        }
    }

    protected abstract void OnClickTransition();
}
