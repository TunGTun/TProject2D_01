using UnityEngine;

public class ToggleObjectOnKey : MyMonoBehaviour
{
	[SerializeField] private GameObject targetObject; // Object bạn muốn bật/tắt

	protected override void Start()
	{
		base.Start();
		targetObject.SetActive(false);
	}
	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadTargetObject();
	}
	protected virtual void LoadTargetObject()
	{
		if (this.targetObject != null) return;
		this.targetObject = GetComponentInChildren<TransitionCtrl>().gameObject;
		Debug.LogWarning(transform.name + ": LoadTargetObject", gameObject);
	}

	void Update()
	{
		if (InputManager.Instance.InteractInput && targetObject != null)
		{
			// Đảo trạng thái bật/tắt
			targetObject.SetActive(!targetObject.activeSelf);
		}
	}

}
