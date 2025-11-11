using System.Collections.Generic;
using UnityEngine;

public class MPSlotCtrl : MySingleton<MPSlotCtrl>
{
    [SerializeField] protected List<Transform> mpSlots;
    [SerializeField] protected List<CurrentMPCtrl> currentMPCtrls;

    [SerializeField] protected int mpSlotEnable = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMPSlots();
        this.LoadCurrentMPCtrls();
    }

    protected virtual void LoadMPSlots()
    {
        if (this.mpSlots.Count > 0) return;

        foreach (Transform mpSlot in this.transform)
        {
            this.mpSlots.Add(mpSlot);
        }

        Debug.Log(transform.name + ": LoadMPSlots", gameObject);
    }

    protected virtual void LoadCurrentMPCtrls()
    {
        if (this.currentMPCtrls.Count > 0) return;

        CurrentMPCtrl currentMPCtrl;

        foreach (Transform mpSlot in this.mpSlots)
        {
            foreach (Transform curMP in mpSlot)
            {
                currentMPCtrl = curMP.GetComponent<CurrentMPCtrl>();
                this.currentMPCtrls.Add(currentMPCtrl);
                break;
            }
        }

        Debug.Log(transform.name + ": LoadCurrentMPCtrls", gameObject);
    }

    protected virtual void HideMPSlots()
    {
        foreach (Transform mpSlot in this.mpSlots)
        {
            mpSlot.gameObject.SetActive(false);
        }
    }

    public virtual void UpdateMaxMP(int maxMP)
    {
        mpSlotEnable = Mathf.CeilToInt(maxMP * 1.0f / SCharStaticData.MaxMP_MPSlot);

        this.HideMPSlots();

        for (int i = 0; i < mpSlotEnable; i++)
        {
            this.mpSlots[i].gameObject.SetActive(true);
        }
    }

    public virtual void UpdateCurrentMPSlot(int currentMP)
    {
        for (int i = 0; i < mpSlotEnable; i++)
        {
            this.currentMPCtrls[i].UpdateCurrentMP(currentMP, i);
        }
    }
}
