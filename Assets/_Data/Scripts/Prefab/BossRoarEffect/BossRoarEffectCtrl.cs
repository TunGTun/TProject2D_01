using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BossRoarEffectCtrl : MyMonoBehaviour
{
    [Header("BossRoarEffectCtrl")]

    [SerializeField] protected BossRoarEffectModel bossRoarEffectModel;
    public BossRoarEffectModel BossRoarEffectModel => bossRoarEffectModel;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossRoarEffectModel();
    }

    protected virtual void LoadBossRoarEffectModel()
    {
        if (this.bossRoarEffectModel != null) return;
        this.bossRoarEffectModel = GetComponentInChildren<BossRoarEffectModel>();
        Debug.LogWarning(transform.name + ": LoadBossRoarEffectModel", gameObject);
    }
}
