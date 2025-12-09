using UnityEngine;

public class M_EnemyHealth : MonoBehaviour
{
    private M_Entity entity;

    [Header("Stats")]
    [SerializeField] protected int maxHP = 55; 
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

    [SerializeField] public bool isDead = false;
    protected virtual void Awake()
    {
        entity = GetComponent<M_Entity>();
        currentHP = maxHP;
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
        { this.CurrentHP = 0; }
        if(CheckIsDead() && !isDead)
        {
            Die();
        }
    }

   

    public bool CheckIsDead()
    {
        if (this.CurrentHP <= 0)
        { return true; }
        return false;
    }
    public virtual void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

}
