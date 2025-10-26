using System.Collections.Generic;
using UnityEngine;

public class CurrentHPCtrl : MySingleton<CurrentHPCtrl>
{
    [SerializeField] protected List<Transform> currentHPs;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCurrentHPs();
    }

    protected virtual void LoadCurrentHPs()
    {
        if (this.currentHPs.Count > 0) return;

        foreach (Transform currentHP in this.transform)
        {
            this.currentHPs.Add(currentHP);
        }

        this.HideCurrentHP();

        Debug.Log(transform.name + ": LoadCurrentHPs", gameObject);
    }

    protected virtual void HideCurrentHP()
    {
        foreach (Transform currentHP in this.currentHPs)
        {
            currentHP.gameObject.SetActive(false);
        }
    }

    public virtual void UpdateCurrentHP(int currentHP)
    {
        for (int i = 0; i < this.currentHPs.Count; i++)
        {
            if (i < currentHP)
                this.currentHPs[i].gameObject.SetActive(true);
            else
                this.currentHPs[i].gameObject.SetActive(false);
        }
    }
}
