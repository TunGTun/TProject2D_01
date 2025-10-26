using System.Collections.Generic;
using UnityEngine;

public class CurrentMPCtrl : MyMonoBehaviour
{
    [SerializeField] protected List<Transform> currentMPs;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCurrentMPs();
    }

    protected virtual void LoadCurrentMPs()
    {
        if (this.currentMPs.Count > 0) return;

        foreach (Transform currentMP in this.transform)
        {
            this.currentMPs.Add(currentMP);
        }

        this.HideCurrentMP();

        Debug.Log(transform.name + ": LoadCurrentMPs", gameObject);
    }

    protected virtual void HideCurrentMP()
    {
        foreach (Transform currentMP in this.currentMPs)
        {
            currentMP.gameObject.SetActive(false);
        }
    }

    public virtual void UpdateCurrentMP(int currentMP, int slot)
    {
        int mpThisSlot = currentMP - SCharStaticData.MaxMP_MPSlot * slot;
        int currentMPEnable = Mathf.Min(mpThisSlot / SCharStaticData.AttackNeedToHeal, SCharStaticData.MaxMP_MPSlot / SCharStaticData.AttackNeedToHeal);

        Debug.Log(currentMPEnable);

        for (int i = 0; i < currentMPs.Count; i++)
        {
            if (i < currentMPEnable)
                this.currentMPs[i].gameObject.SetActive(true);
            else
                this.currentMPs[i].gameObject.SetActive(false);
        }
    }
}
