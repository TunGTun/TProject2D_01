using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentMPCtrl : MyMonoBehaviour
{
    [SerializeField] protected List<Transform> currentMPs;

    [SerializeField] protected List<Image> currentMPImages;

    [SerializeField] protected float duration = 0.5f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCurrentMPs();
        this.LoadCurrentMPImages();
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

    protected virtual void LoadCurrentMPImages()
    {
        if (this.currentMPImages.Count > 0) return;

        foreach (Transform currentMP in currentMPs)
        {
            Image curImg = currentMP.GetComponent<Image>();
            this.currentMPImages.Add(curImg);
        }

        Debug.LogWarning(transform.name + ": LoadCurrentMPImages", gameObject);
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

        for (int i = 0; i < currentMPs.Count; i++)
        {
            if (i < currentMPEnable)
            {
                if (!this.currentMPs[i].gameObject.activeSelf)
                    StartCoroutine(CurrentMPEnableRoutine(this.currentMPs[i], this.currentMPImages[i]));
            }
            else
            {
                if (this.currentMPs[i].gameObject.activeSelf)
                    StartCoroutine(CurrentMPDisableRoutine(this.currentMPs[i], this.currentMPImages[i]));
            }
        }
    }

    protected virtual IEnumerator CurrentMPEnableRoutine(Transform currentMP, Image currentMPImage)
    {
        this.SetAlpha(currentMPImage, 0f);

        currentMP.gameObject.SetActive(true);

        currentMPImage.DOFade(1f, duration);

        yield return null;
    }

    protected virtual IEnumerator CurrentMPDisableRoutine(Transform currentMP, Image currentMPImage)
    {
        currentMPImage.DOFade(0f, duration);

        yield return new WaitForSeconds(this.duration);

        currentMP.gameObject.SetActive(false);
    }

    private void SetAlpha(Image image, float value)
    {
        Color c = image.color;
        c.a = value;
        image.color = c;
    }
}
