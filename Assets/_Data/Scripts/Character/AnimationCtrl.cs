using UnityEngine;

public class AnimationCtrl : BaseChar
{
    [Header("AnimationCtrl")]

    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    [SerializeField] protected string currentAnim;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    protected virtual void LoadAnimator()
    {
        if (animator != null) return;
        animator = GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void ChangeAnimationState(string newState)
    {
        if (currentAnim == newState) return;

        this.animator.Play(newState);

        currentAnim = newState;
    }

    public virtual void UpdateAnimation()
    {
        IState<CharBaseState> highestPriorityState = this.charCtrl.CharStateCtrl.GetHighestPriorityState();
        this.ChangeAnimationState(highestPriorityState.Name);
    }
}
