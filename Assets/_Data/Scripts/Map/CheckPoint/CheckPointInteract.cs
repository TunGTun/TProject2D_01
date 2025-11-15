using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointInteract : MyMonoBehaviour
{
    [SerializeField] protected bool isActive = false;
    public bool IsActive => isActive;

    [SerializeField] protected CheckPointCtrl checkPointCtrl;
    public CheckPointCtrl CheckPointCtrl => checkPointCtrl;

    protected bool isPlayerInRange = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCheckPointCtrl();
    }

    protected virtual void LoadCheckPointCtrl()
    {
        if (this.checkPointCtrl != null) return;
        this.checkPointCtrl = GetComponentInParent<CheckPointCtrl>(true);
        Debug.Log(transform.name + ": LoadCheckPointCtrl", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.Init();
    }

    protected virtual void Init()
    {
        this.isActive = SaveLoadSceneData.Instance.SceneData.IsCheckPointActive;

        Vector3 temp1 = this.checkPointCtrl.CheckPointWorldCanvas.ActiveGuide.localScale;
        temp1.y = 0f;
        this.checkPointCtrl.CheckPointWorldCanvas.ActiveGuide.localScale = temp1;

        Vector3 temp2 = this.checkPointCtrl.CheckPointWorldCanvas.SoulLinkGuide.localScale;
        temp2.y = 0f;
        this.checkPointCtrl.CheckPointWorldCanvas.SoulLinkGuide.localScale = temp2;

        this.SetUpActive();
    }

    protected virtual void SetUpActive()
    {
        if (this.isActive) return;
        this.checkPointCtrl.CheckPointPortal.localScale = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        this.isPlayerInRange = true;

        if (this.isActive)
        {
            this.checkPointCtrl.CheckPointWorldCanvas.SoulLinkGuide.DOScaleY(0.01f, 0.5f);
        }
        else
        {
            this.checkPointCtrl.CheckPointWorldCanvas.ActiveGuide.DOScaleY(0.01f, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        this.isPlayerInRange = false;

        if (this.isActive)
        {
            this.checkPointCtrl.CheckPointWorldCanvas.SoulLinkGuide.DOScaleY(0f, 0.5f);
        }
        else
        {
            this.checkPointCtrl.CheckPointWorldCanvas.ActiveGuide.DOScaleY(0f, 0.5f);
        }
    }

    private void Update()
    {
        this.Interact();
    }

    protected virtual void Interact()
    {
        if (!this.isPlayerInRange) return;
        if (!InputManager.Instance.InteractInput) return;
        if (!this.isActive) this.Active();
        else StartCoroutine(TransitionRoutine());
    }

    protected virtual void Active()
    {
        this.isActive = true;
        SaveLoadSceneData.Instance.SceneData.IsCheckPointActive = this.isActive;
        this.checkPointCtrl.CheckPointPortal.DOScale(Vector3.one, 0.5f);
        this.checkPointCtrl.CheckPointWorldCanvas.ActiveGuide.DOScaleY(0f, 0.5f)
            .OnComplete(() =>
            {
                this.checkPointCtrl.CheckPointWorldCanvas.SoulLinkGuide.DOScaleY(0.01f, 0.5f);
            });
    }

    protected virtual void SoulLink()
    {
        Time.timeScale = 0;
        this.checkPointCtrl.CheckPointPanel.SetActive(true);

        CharCtrl.Instance.CharData.AddHP(CharCtrl.Instance.CharData.MaxHP - CharCtrl.Instance.CharData.CurrentHP);

        SaveLoadManager.Instance.SavePlayer();
        SaveLoadSceneData.Instance.SaveScene();
    }

    protected virtual IEnumerator TransitionRoutine()
    {
        CharCtrl.Instance.CharStateCtrl.StatusState.ChangeState(CharCtrl.Instance.CharStateCtrl.StatusState.soulLink);
        InputManager.Instance.InteractInput = false;

        yield return new WaitForSeconds(SCharStaticData.SoulLinkDuration);
        Animator animator = MySceneManager.Instance.SceneTransitionAnimator;
        animator.SetTrigger(SSceneTransitionData.ANIMATION_END_TRIGGER);

        yield return new WaitForSeconds(SSceneTransitionData.AnimationDuration);
        this.SoulLink();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetTrigger(SSceneTransitionData.ANIMATION_START_TRIGGER);
    }

    public virtual void Unlink()
    {
        StartCoroutine(UnlinkRoutine());
    }

    protected virtual IEnumerator UnlinkRoutine()
    {
        Time.timeScale = 1;
        this.checkPointCtrl.CheckPointPanel.SetActive(false);
        yield return new WaitForSeconds(SSceneTransitionData.AnimationDuration);
        CharCtrl.Instance.CharStateCtrl.StatusState.ChangeState(CharCtrl.Instance.CharStateCtrl.StatusState.spawn);
    }
}
