using UnityEngine;

public class BossAnimationCtrl : MyMonoBehaviour
{
    [Header("BossAnimationCtrl")]

    [SerializeField] protected BaseBossCtrl baseBossCtrl;
    public BaseBossCtrl BaseBossCtrl => baseBossCtrl;

    [SerializeField] protected SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer => spriteRenderer;

    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    [SerializeField] protected string currentAnim;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBaseBossCtrl();
        this.LoadSpriteRenderer();
        this.LoadAnimator();
    }

    protected virtual void LoadBaseBossCtrl()
    {
        if (baseBossCtrl != null) return;
        baseBossCtrl = GetComponentInParent<BaseBossCtrl>();
        Debug.LogWarning(transform.name + ": LoadBaseBossCtrl", gameObject);
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
}
