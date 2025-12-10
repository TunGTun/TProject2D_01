using UnityEngine;
using UnityEngine.UI;

public class TShopUI : MyMonoBehaviour
{
	[Header("Shop UI")]
	[SerializeField] private GameObject shopPanel;
	[SerializeField] private Button exitButton; // Button TextMeshPro

	private bool _isOpen = false;
	private CharStateCtrl _charStateCtrl;

	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadShopPanel();
		this.LoadExitButton();
		this.LoadCharStateCtrl();
	}

	protected virtual void LoadShopPanel()
	{
		if (this.shopPanel != null) return;

		this.shopPanel = this.transform.Find("ShopPanel")?.gameObject;
		Debug.LogWarning(transform.name + ": LoadShopPanel", this.gameObject);
	}

	protected virtual void LoadExitButton()
	{
		if (this.exitButton != null) return;

		this.exitButton = this.GetComponentInChildren<Button>();
		Debug.LogWarning(transform.name + ": LoadExitButton", this.gameObject);
	}

	protected virtual void LoadCharStateCtrl()
	{
		if (this._charStateCtrl != null) return;

		this._charStateCtrl = FindObjectOfType<CharStateCtrl>();
		Debug.LogWarning(transform.name + ": LoadCharStateCtrl", this.gameObject);
	}

	protected override void Start()
	{
		base.Start();
		this.shopPanel.SetActive(false);

		if (this.exitButton != null)
			this.exitButton.onClick.AddListener(this.CloseShop);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Y))
		{
			this.OpenShop();
		}
	}

	public virtual void OpenShop()
	{
		this._isOpen = true;
		this.shopPanel.SetActive(true);

		// Khóa điều khiển nhân vật bằng CUTSCENE
		this._charStateCtrl.StatusState.ChangeState(this._charStateCtrl.StatusState.cutScene);

		Debug.Log("Shop opened");
	}

	public virtual void CloseShop()
	{
		if (!this._isOpen) return;

		this._isOpen = false;
		this.shopPanel.SetActive(false);

		// Mở lại điều khiển nhân vật
		this._charStateCtrl.StatusState.ChangeState(this._charStateCtrl.StatusState.normal);

		Debug.Log("Shop closed");
	}
}
