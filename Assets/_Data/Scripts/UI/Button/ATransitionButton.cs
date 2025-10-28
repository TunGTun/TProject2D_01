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
        MySceneManager.Instance.SceneTransitionAnimator.SetTrigger(SSceneTransitionData.ANIMATION_END_TRIGGER);
        yield return new WaitForSeconds(SSceneTransitionData.AnimationDuration);
        this.OnClickTransition();
        MySceneManager.Instance.SceneTransitionAnimator.SetTrigger(SSceneTransitionData.ANIMATION_START_TRIGGER);
    }

    protected abstract void OnClickTransition();
}
