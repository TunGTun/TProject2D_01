using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ABaseSlider : MyMonoBehaviour
{
    [Header("ABaseSlider")]
    [SerializeField] protected Slider slider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSlider();
    }

    protected virtual void LoadSlider()
    {
        if (this.slider != null) return;
        this.slider = GetComponent<Slider>();
        Debug.Log(transform.name + ": LoadSlider", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.AddOnValueChangedEvent();
    }

    protected virtual void AddOnValueChangedEvent()
    {
        this.slider.onValueChanged.AddListener(this.OnValueChanged);
    }

    protected abstract void OnValueChanged(float value);
}
