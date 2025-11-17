using TMPro;
using UnityEngine;

/// <summary>
/// Vietnamese: NPC nói 3 câu theo thứ tự và lặp lại.
/// </summary>
public class TNpcDialogue : MyMonoBehaviour
{
	[Header("Dialogue Settings")]
	[SerializeField] private string[] dialogues = new string[3]; // 3 câu thoại
	[SerializeField] private float dialogueDelay = 2f; // Thời gian giữa mỗi câu
	[SerializeField] private TextMeshProUGUI dialogueText; // Text UI để hiển thị

	private int _dialogueIndex = 0; // Chỉ số câu hiện tại
	private float _timer = 0f; // Bộ đếm thời gian

	protected override void Start()
	{
		base.Start();
		this.ShowDialogue();
	}

	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadDialogueText();
	}

	private void LoadDialogueText()
	{
		if (this.dialogueText != null) return;

		this.dialogueText = this.GetComponentInChildren<TextMeshProUGUI>();
		// Vietnamese: Tự tìm Text nếu chưa kéo vào
	}

	private void Update()
	{
		this._timer += Time.deltaTime;

		if (this._timer >= this.dialogueDelay)
		{
			this._timer = 0f;
			this.NextDialogue();
		}
	}

	private void NextDialogue()
	{
		this._dialogueIndex++;

		if (this._dialogueIndex >= this.dialogues.Length)
		{
			this._dialogueIndex = 0;
			// Vietnamese: Lặp lại từ đầu
		}

		this.ShowDialogue();
	}

	private void ShowDialogue()
	{
		if (this.dialogueText == null) return;

		this.dialogueText.text = this.dialogues[this._dialogueIndex];
		// Vietnamese: Hiển thị câu thoại hiện tại
	}
}
