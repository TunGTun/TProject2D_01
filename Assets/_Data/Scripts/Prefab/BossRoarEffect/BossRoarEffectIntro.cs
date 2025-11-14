using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BossRoarEffectIntro : MyMonoBehaviour
{
    [Header("BossRoarEffectIntro")]

    [SerializeField] protected BossRoarEffectCtrl bossRoarEffectCtrl;
    public BossRoarEffectCtrl BossRoarEffectCtrl => bossRoarEffectCtrl;

    [Header("Roar Effect Settings")]
    [SerializeField] protected float duration = 2.04f;
    [SerializeField] protected Vector3 startScale = Vector3.one;
    [SerializeField] protected Vector3 endScale = Vector3.one * 10f;

    private List<Tween> activeTweens = new List<Tween>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossRoarEffectCtrl();
    }

    protected virtual void LoadBossRoarEffectCtrl()
    {
        if (this.bossRoarEffectCtrl != null) return;
        this.bossRoarEffectCtrl = GetComponentInParent<BossRoarEffectCtrl>();
        Debug.LogWarning(transform.name + ": LoadBossRoarEffectCtrl", gameObject);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        for (int i = 0; i < this.bossRoarEffectCtrl.BossRoarEffectModel.BossRoarEffectModels.Count; i++)
        {
            this.bossRoarEffectCtrl.BossRoarEffectModel.BossRoarEffectModels[i].localScale = Vector3.zero;
            var sr = this.bossRoarEffectCtrl.BossRoarEffectModel.BossRoarEffectSprites[i];
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }

        StartRoarEffect();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        foreach (var t in activeTweens) t?.Kill();
        activeTweens.Clear();
    }

    protected void StartRoarEffect()
    {
        int waveCount = this.bossRoarEffectCtrl.BossRoarEffectModel.BossRoarEffectModels.Count - 1;

        float doTime = (duration * 2f) / (waveCount + 1);
        float delay = doTime / 2f;

        var models = this.bossRoarEffectCtrl.BossRoarEffectModel.BossRoarEffectModels;
        var sprites = this.bossRoarEffectCtrl.BossRoarEffectModel.BossRoarEffectSprites;

        {
            Transform model0 = models[0];
            SpriteRenderer sr0 = sprites[0];

            Sequence seq0 = DOTween.Sequence();

            seq0.AppendCallback(() =>
            {
                model0.localScale = startScale;
                sr0.color = new Color(sr0.color.r, sr0.color.g, sr0.color.b, 1f);
            });

            seq0.Append(model0.DOScale(endScale, doTime * 3f));
            seq0.Join(sr0.DOFade(0f, doTime * 3f));

            seq0.Play();
            activeTweens.Add(seq0);
        }

        for (int i = 1; i < models.Count; i++)
        {
            Transform model = models[i];
            SpriteRenderer sr = sprites[i];

            Sequence seq = DOTween.Sequence();

            seq.AppendInterval(delay * (i - 1));

            seq.AppendCallback(() =>
            {
                model.localScale = startScale;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            });

            seq.Append(model.DOScale(endScale, doTime));
            seq.Join(sr.DOFade(0f, doTime));

            seq.Play();
            activeTweens.Add(seq);
        }
    }
}
