using UnityEngine;

public class TDialogueTrigger : MyMonoBehaviour
{
	protected TDialogueNPC _npc;

	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadNPC();
	}

	protected virtual void LoadNPC()
	{
		if (this._npc != null) return;
		this._npc = this.GetComponentInParent<TDialogueNPC>();
	}

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player")) return;

		this._npc.SetPlayerInRange(true);
		TDialogueManager.Instance.ShowPressR(true);
	}

	protected void OnTriggerExit2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player")) return;

		this._npc.SetPlayerInRange(false);
		TDialogueManager.Instance.ShowPressR(false);
	}
}
