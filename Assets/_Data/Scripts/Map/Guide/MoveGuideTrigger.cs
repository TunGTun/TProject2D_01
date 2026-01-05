using DG.Tweening;
using UnityEngine;

public class MoveGuideTrigger : MyMonoBehaviour
{
    [SerializeField] protected Transform moveGuideModel;
    public Transform MoveGuideModel => moveGuideModel;

    [Header("Guide Settings")]
    [SerializeField] protected float requireStayTime = 8f;
    [SerializeField] protected float scaleDuration = 0.5f;

    protected float stayTimer = 0f;
    protected bool isPlayerInside = false;
    protected Tween scaleTween;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMoveGuideModel();
    }

    protected virtual void LoadMoveGuideModel()
    {
        if (this.moveGuideModel != null) return;
        
        foreach (Transform t in transform)
        {
            this.moveGuideModel = t;
            break;
        }

        Debug.LogWarning(transform.name + ": LoadMoveGuideModel", gameObject);
    }

    protected override void Start()
    {
        base.Start();

        Vector3 temp = moveGuideModel.localScale;
        temp.y  = 0f;
        moveGuideModel.localScale = temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        isPlayerInside = true;
        stayTimer = 0f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        isPlayerInside = false;
        stayTimer = 0f;

        scaleTween?.Kill();
    }

    private void Update()
    {
        if (!isPlayerInside) return;

        stayTimer += Time.deltaTime;

        if (stayTimer >= requireStayTime)
        {
            if (scaleTween == null || !scaleTween.IsPlaying())
            {
                RunScaleUp();
            }
        }
    }

    protected void RunScaleUp()
    {
        scaleTween = moveGuideModel.DOScaleY(1f, scaleDuration);
    }
}
