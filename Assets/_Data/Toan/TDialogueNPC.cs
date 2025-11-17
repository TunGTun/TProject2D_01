using UnityEngine;

public class TDialogueNPC : MyMonoBehaviour
{
	[Header("Dialogue Settings")]
	[SerializeField] protected string[] dialogues; // các câu thoại
	public string[] Dialogues => this.dialogues;

	[SerializeField] protected string npcName = "NPC";
	public string NpcName => this.npcName;

	protected bool _playerInRange = false;

	protected override void LoadComponents()
	{
		base.LoadComponents();
		// không cần load gì thêm
	}

	protected void Update()
	{
		if (!this._playerInRange) return;

		if (Input.GetKeyDown(KeyCode.R))
		{
			TDialogueManager.Instance.StartDialogue(this);
		}
	}

	public void SetPlayerInRange(bool value)
	{
		this._playerInRange = value;
	}
}
