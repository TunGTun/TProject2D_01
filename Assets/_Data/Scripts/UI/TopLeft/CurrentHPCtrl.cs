using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CurrentHPCtrl : MySingleton<CurrentHPCtrl>
{
    [SerializeField] protected List<Transform> currentHPs;

    [SerializeField] protected float duration = 0.5f;

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
            {
                if (!this.currentHPs[i].gameObject.activeSelf)
                    StartCoroutine(CurrentHPEnableRoutine(this.currentHPs[i]));
            }
            else
            {
                if (this.currentHPs[i].gameObject.activeSelf)
                    StartCoroutine(CurrentHPDisableRoutine(this.currentHPs[i]));
            }
        }
    }

    protected virtual IEnumerator CurrentHPEnableRoutine(Transform currentHP)
    {
        currentHP.localScale = Vector3.zero;

        currentHP.gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();
        seq.Append(currentHP.DOScale(Vector3.one, duration - 0.4f * duration));
        seq.Join(currentHP.DORotate(new Vector3(0, 0, -360), duration - 0.4f * duration, RotateMode.FastBeyond360));

        yield return null;
    }

    protected virtual IEnumerator CurrentHPDisableRoutine(Transform currentHP)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(currentHP.DOScale(Vector3.zero, duration));
        seq.Join(currentHP.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360));

        yield return new WaitForSeconds(this.duration);

        currentHP.gameObject.SetActive(false);
    }
}
