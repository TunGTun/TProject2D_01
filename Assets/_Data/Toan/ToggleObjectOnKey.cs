using System.Collections;
using UnityEngine;

public class ToggleObjectOnKey : MyMonoBehaviour
{
	[SerializeField] private GameObject targetObject;

	protected override void Start()
	{
		base.Start();
		StartCoroutine(TargetActiveRoutine());
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
			targetObject.SetActive(!targetObject.activeSelf);
		}
	}

	protected virtual IEnumerator TargetActiveRoutine()
	{
        yield return new WaitForSeconds(1f);
        targetObject.SetActive(false);
    }

}
