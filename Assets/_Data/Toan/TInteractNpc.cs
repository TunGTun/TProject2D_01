using TMPro;
using UnityEngine;

public class TInteractNpc : MyMonoBehaviour
{
	[Header("Dialogue")]
	[SerializeField] private GameObject bubble;
	[SerializeField] private TextMeshProUGUI dialogueText;
	[SerializeField] private string[] dialogues;

	private int dialogueIndex = 0;
	private bool playerInRange = false;
	private bool dialogueFinished = false;

	[Header("Shop UI")]
	[SerializeField] private GameObject shopUI; // UI cửa hàng nhỏ

	protected override void LoadComponents()
	{
		base.LoadComponents();

		if (this.bubble == null)
			this.bubble = transform.Find("WorldCanvas/SpeechBubble")?.gameObject;

		if (this.dialogueText == null)
			this.dialogueText = transform.Find("WorldCanvas/SpeechBubble/Chat/Text")
				?.GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		if (!this.playerInRange) return;

		// Nếu thoại chưa xong → bấm R để xem tiếp
		if (!dialogueFinished && Input.GetKeyDown(KeyCode.R))
		{
			ShowNextDialogue();
			return;
		}

		// Nếu thoại đã xong → chờ người chơi chọn Y/N
		if (dialogueFinished)
		{
			if (Input.GetKeyDown(KeyCode.Y))
			{
				OpenShop();
			}
			else if (Input.GetKeyDown(KeyCode.N))
			{
				EndDialogue();
			}
		}
	}

	private void ShowNextDialogue()
	{
		if (dialogues.Length == 0) return;

		bubble.SetActive(true);

		dialogueText.text = dialogues[dialogueIndex];

		dialogueIndex++;

		// Nếu đã đến câu cuối → không tăng nữa
		if (dialogueIndex >= dialogues.Length)
		{
			dialogueFinished = true;
			Debug.Log("Đã hết câu thoại. Nhấn Y để mở shop, N để kết thúc.");
		}
	}

	public void SetPlayerInRange(bool inRange)
	{
		playerInRange = inRange;

		if (!inRange)
		{
			HideBubble();
			dialogueFinished = false;
			dialogueIndex = 0; // reset khi rời khỏi NPC
		}
	}

	private void OpenShop()
	{
		Debug.Log("Mở cửa hàng!");
		shopUI.SetActive(true);
		HideBubble();
	}

	private void EndDialogue()
	{
		Debug.Log("Kết thúc hội thoại!");
		HideBubble();
		dialogueFinished = false;
		dialogueIndex = 0;
	}

	private void HideBubble()
	{
		if (bubble != null)
			bubble.SetActive(false);
	}
}
