using UnityEngine;

public class M_EnemyHealth : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int maxHP = 5; 
    public int MaxHP
    {
        get => maxHP;
        set
        {
            maxHP = value;
            HPSlotCtrl.Instance.UpdateMaxHP(this.MaxHP);
        }
    }



    [Header("Current Stats")]
    [SerializeField] protected int currentHP;
    public int CurrentHP
    {
        get => currentHP;
        set
        {
            currentHP = value;
            CurrentHPCtrl.Instance.UpdateCurrentHP(this.currentHP);
        }
    }


    public virtual void AddHP(int hp)
    {
        this.CurrentHP += hp;
        if (this.CurrentHP > this.maxHP)
            this.CurrentHP = this.maxHP;
    }

    public virtual void SubHP(int damage)
    {
        this.CurrentHP -= damage;
        if (this.CurrentHP < 0)
            this.CurrentHP = 0;
    }

    [SerializeField] protected bool isDead = false;

    public bool CheckIsDead()
    {
        if (this.CurrentHP <= 0)
        { return true; }
        return false;
    }
    public virtual void Die()
    {
        if (isDead) return;
        isDead = true;
        // die animation
        // drop money
        // destroy object
    }

}
