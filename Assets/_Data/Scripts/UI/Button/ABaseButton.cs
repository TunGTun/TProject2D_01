using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ABaseButton : MyMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("ABaseButton")]
    [SerializeField] protected Button button;

    protected override void Start()
    {
        base.Start();
        this.AddOnClickEvent();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButton();
    }

    protected virtual void LoadButton()
    {
        if (this.button != null) return;
        this.button = GetComponent<Button>();
        Debug.Log(transform.name + ": LoadButton", gameObject);
    }

    protected virtual void AddOnClickEvent()
    {
        this.button.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(ESoundName.ButtonClick);
            this.OnClick();
            EventSystem.current.SetSelectedGameObject(null);
        });
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.OnHoverEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.OnHoverExit();
    }

    protected abstract void OnClick();

    protected virtual void OnHoverEnter()
    {
        AudioManager.Instance.PlaySFX(ESoundName.ButtonHover);
    }

    protected virtual void OnHoverExit()
    {

    }

}
