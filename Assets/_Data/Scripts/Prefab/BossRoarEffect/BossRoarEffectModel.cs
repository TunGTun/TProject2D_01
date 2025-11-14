using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BossRoarEffectModel : MyMonoBehaviour
{
    [Header("BossRoarEffectModel")]

    [SerializeField] protected List<Transform> bossRoarEffectModels;
    public List<Transform> BossRoarEffectModels => bossRoarEffectModels;

    [SerializeField] protected List<SpriteRenderer> bossRoarEffectSprites;
    public List<SpriteRenderer> BossRoarEffectSprites => bossRoarEffectSprites;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossRoarEffectModels();
    }

    protected virtual void LoadBossRoarEffectModels()
    {
        if (bossRoarEffectModels.Count > 0) return;
        foreach (Transform roarEffectModel in this.transform)
        {
            bossRoarEffectModels.Add(roarEffectModel);
            bossRoarEffectSprites.Add(roarEffectModel.GetComponent<SpriteRenderer>());
        }
        Debug.LogWarning(transform.name + ": LoadBossRoarEffectModels", gameObject);
    }
}