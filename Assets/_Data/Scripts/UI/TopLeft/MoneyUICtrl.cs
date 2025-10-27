using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUICtrl : MySingleton<MoneyUICtrl>
{
    [SerializeField] protected TextMeshProUGUI moneyText;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMoneyText();
    }

    protected virtual void LoadMoneyText()
    {
        if (this.moneyText != null) return;

        this.moneyText = GetComponentInChildren<TextMeshProUGUI>();

        Debug.Log(transform.name + ": LoadMoneyText", gameObject);
    }

    public virtual void SetMoneyText(int money)
    {
        this.moneyText.text = money.ToString();
    }
}
