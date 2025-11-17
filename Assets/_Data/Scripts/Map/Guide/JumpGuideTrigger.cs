using DG.Tweening;
using UnityEngine;

public class JumpGuideTrigger : MyMonoBehaviour
{
    [SerializeField] protected Transform jumpGuideModel;
    public Transform JumpGuideModel => jumpGuideModel;

    [Header("Guide Settings")]
    [SerializeField] protected float requireStayTime = 5f;
    [SerializeField] protected float scaleDuration = 0.5f;

    protected float stayTimer = 0f;
    protected bool isPlayerInside = false;
    protected bool hasPlayed = false;
    protected Tween scaleTween;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMoveGuideModel();
    }

    protected virtual void LoadMoveGuideModel()
    {
        if (this.jumpGuideModel != null) return;

        foreach (Transform t in transform)
        {
            this.jumpGuideModel = t;
            break;
        }

        Debug.LogWarning(transform.name + ": LoadMoveGuideModel", gameObject);
    }

    protected override void Start()
    {
        base.Start();

        Vector3 temp = jumpGuideModel.localScale;
        temp.y = 0f;
        jumpGuideModel.localScale = temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (hasPlayed) return;

        isPlayerInside = true;
        stayTimer = 0f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (hasPlayed) return;

        isPlayerInside = false;
        stayTimer = 0f;
    }

    private void Update()
    {
        if (!isPlayerInside) return;
        if (hasPlayed) return;

        stayTimer += Time.deltaTime;

        if (stayTimer >= requireStayTime)
        {
            PlayOnce();
        }
    }

    protected void PlayOnce()
    {
        if (hasPlayed) return;

        hasPlayed = true;

        scaleTween?.Kill();
        scaleTween = jumpGuideModel.DOScaleY(1f, scaleDuration);
    }
}
