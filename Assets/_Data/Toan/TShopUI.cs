using UnityEngine;

public class TShopUI : MyMonoBehaviour
{
	[SerializeField] private GameObject shopPanel;

	private bool isOpen = false;

	protected override void Start()
	{
		base.Start();
		shopPanel.SetActive(false);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Y))
		{
			OpenShop();
		}
	}

	public void OpenShop()
	{
		isOpen = true;
		shopPanel.SetActive(true);
		CharCtrl.Instance.CharStateCtrl.StatusState.ChangeState(CharCtrl.Instance.CharStateCtrl.StatusState.cutScene);
	}

	public void CloseShop()
	{
		isOpen = false;
		shopPanel.SetActive(false);
		CharCtrl.Instance.CharStateCtrl.StatusState.ChangeState(CharCtrl.Instance.CharStateCtrl.StatusState.normal);
	}
}
