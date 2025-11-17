using UnityEngine;

public class TNpcRange : MyMonoBehaviour
{
	[SerializeField] private TInteractNpc interact;

	protected override void LoadComponents()
	{
		base.LoadComponents();
		if (this.interact == null)
			this.interact = transform.parent.GetComponent<TInteractNpc>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Trigger enter: " + collision.name);

		if (collision.CompareTag("Player"))
		{
			Debug.Log("Player vào vùng");
			this.interact.SetPlayerInRange(true);
		}
	}


	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			this.interact.SetPlayerInRange(false);
	}
}
