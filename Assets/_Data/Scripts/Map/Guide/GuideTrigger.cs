using DG.Tweening;
using UnityEngine;

public class GuideTrigger : MyMonoBehaviour
{
    [SerializeField] protected Transform guideModel;
    public Transform GuideModel => guideModel;

    [Header("Guide Settings")]
    [SerializeField] protected float scaleDuration = 0.5f;

    protected Tween scaleTween;
    protected bool hasPlayed = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMoveGuideModel();
    }

    protected virtual void LoadMoveGuideModel()
    {
        if (this.guideModel != null) return;

        foreach (Transform t in transform)
        {
            this.guideModel = t;
            break;
        }

        Debug.LogWarning(transform.name + ": LoadMoveGuideModel", gameObject);
    }

    protected override void Start()
    {
        base.Start();

        Vector3 temp = guideModel.localScale;
        temp.y = 0f;
        guideModel.localScale = temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (hasPlayed) return;

        hasPlayed = true;

        scaleTween?.Kill();

        scaleTween = guideModel.DOScaleY(1f, scaleDuration);
    }
}
