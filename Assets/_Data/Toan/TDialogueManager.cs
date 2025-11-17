using UnityEngine;
using UnityEngine.UI;

public class TDialogueManager : MyMonoBehaviour
{
	[Header("UI References")]
	[SerializeField] protected GameObject dialoguePanel;
	[SerializeField] protected Text dialogueText;
	[SerializeField] protected Text nameText;
	[SerializeField] protected GameObject pressRPanel;

	protected int _currentIndex = 0;
	protected string[] _currentDialogues;
	protected bool _isTalking = false;

	// Singleton
	public static TDialogueManager Instance;

	protected override void Awake()
	{
		base.Awake();
		Instance = this;

		this.dialoguePanel.SetActive(false);
		this.pressRPanel.SetActive(false);
	}

	protected override void LoadComponents()
	{
		base.LoadComponents();
		// các UI sẽ set trong inspector
	}

	public void ShowPressR(bool show)
	{
		this.pressRPanel.SetActive(show && !_isTalking);
	}

	public void StartDialogue(TDialogueNPC npc)
	{
		this._currentDialogues = npc.Dialogues;
		this._currentIndex = 0;
		this._isTalking = true;

		this.pressRPanel.SetActive(false);
		this.dialoguePanel.SetActive(true);

		this.nameText.text = npc.NpcName;
		this.dialogueText.text = this._currentDialogues[0];
	}

	protected void Update()
	{
		if (!this._isTalking) return;

		if (Input.GetKeyDown(KeyCode.R))
		{
			this.NextDialogue();
		}
	}

	protected void NextDialogue()
	{
		this._currentIndex++;

		if (this._currentIndex >= this._currentDialogues.Length)
		{
			this.EndDialogue();
			return;
		}

		this.dialogueText.text = this._currentDialogues[this._currentIndex];
	}

	protected void EndDialogue()
	{
		this._isTalking = false;
		this.dialoguePanel.SetActive(false);
	}
}
