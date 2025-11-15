using UnityEngine;

public class AnimationCtrl : BaseChar
{
    [Header("AnimationCtrl")]

    [SerializeField] protected SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer => spriteRenderer;

    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    [SerializeField] protected string currentAnim;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpriteRenderer();
        this.LoadAnimator();
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (spriteRenderer != null) return;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.LogWarning(transform.name + ": LoadSpriteRenderer", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (animator != null) return;
        animator = GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    public virtual void ChangeAnimationState(string newState)
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
