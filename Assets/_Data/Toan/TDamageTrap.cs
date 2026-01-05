using UnityEngine;

public class TDamageTrap : MyMonoBehaviour
{
	[Header("Trap Settings")]
	[SerializeField] private int damage = 1;
	[SerializeField] private float cooldown = 1f;

	private bool _canDamage = true;

	// Player
	private CharCtrl _charCtrl;
	private CharData _charData;

	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadCharCtrl();
		this.LoadCharData();
	}

	protected virtual void LoadCharCtrl()
	{
		if (this._charCtrl != null) return;

		this._charCtrl = CharCtrl.Instance;

		if (this._charCtrl == null)
			Debug.LogError(this.transform.name + ": CharCtrl NOT FOUND");
		else
			Debug.Log("Trap found CharCtrl: " + this._charCtrl.name);
	}

	protected virtual void LoadCharData()
	{
		if (this._charData != null) return;
		if (this._charCtrl == null) return;

		this._charData = this._charCtrl.CharData;

		if (this._charData == null)
			Debug.LogError(this.transform.name + ": CharData NOT FOUND in CharCtrl");
		else
			Debug.Log("Trap loaded HP: " + this._charData.CurrentHP);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!this._canDamage) return;

		// Kiểm tra đúng player hay không (so sánh transform root)
		if (other.transform.root != this._charCtrl.transform.root) return;

		this.DealDamage();
	}

	protected virtual void DealDamage()
	{
		if (this._charData == null)
		{
			Debug.LogError("Trap: CharData missing!");
			return;
		}

		int before = this._charData.CurrentHP;

		this._charData.AddHP(-this.damage);

		Debug.Log("Trap Damage → HP: " + before + " → " + this._charData.CurrentHP);

		this.StartCooldown();
	}

	protected virtual void StartCooldown()
	{
		this._canDamage = false;
		this.Invoke(nameof(ResetDamage), this.cooldown);
	}

	protected virtual void ResetDamage()
	{
		this._canDamage = true;
	}
}
