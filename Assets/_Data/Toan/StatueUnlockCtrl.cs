using UnityEngine;

public class StatueUnlockCtrl : MyMonoBehaviour
{
	[Header("Settings")]
	// Chọn Dash hoặc DoubleJump ở đây
	[SerializeField] protected ESkill skillToUnlock = ESkill.None;

	private bool _isPlayerNearby;

	private void Update()
	{
		// Người ở gần + Bấm R
		if (this._isPlayerNearby && Input.GetKeyDown(KeyCode.R))
		{
			this.UnlockSkill();
		}
	}

	protected virtual void UnlockSkill()
	{
		CharCtrl.Instance.CharStateCtrl.SkillLock.UnlockSkill(skillToUnlock);
		Debug.Log(transform.name + ": Đã mở khóa -> " + this.skillToUnlock);
	}

	// --- Physics ---

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
			this._isPlayerNearby = true;

	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
			this._isPlayerNearby = false;

	}
}