using System.Collections.Generic;
using UnityEngine;

public class HPSlotCtrl : MySingleton<HPSlotCtrl>
{
    [SerializeField] protected List<Transform> hpSlots;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHPSlots();
    }

    protected virtual void LoadHPSlots()
    {
        if (this.hpSlots.Count > 0) return;

        foreach (Transform hpSlot in this.transform)
        {
            this.hpSlots.Add(hpSlot);
        }

        Debug.Log(transform.name + ": LoadHPSlots", gameObject);
    }

    protected virtual void HideHPSlots()
    {
        foreach (Transform hpSlot in this.hpSlots)
        {
            hpSlot.gameObject.SetActive(false);
        }
    }

    public virtual void UpdateMaxHP(int maxHP)
    {
        this.HideHPSlots();

        for (int i = 0; i < maxHP; i++)
        {
            this.hpSlots[i].gameObject.SetActive(true);
        }
    }
}
