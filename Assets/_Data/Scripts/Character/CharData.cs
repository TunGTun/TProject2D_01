using UnityEngine;

public class CharData : MyMonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int maxHP = 5; //toi da 15
    public int MaxHP => maxHP;

    [SerializeField] protected int maxMP = 100; // toi da 300
    public int MaxMP => maxMP;

    [SerializeField] protected int attackDamage = 10;
    public int AttackDamage => attackDamage;

    [Header("Current Stats")]
    [SerializeField] protected int currentHP;
    public int CurrentHP { get => currentHP; set => currentHP = value; }

    [SerializeField] protected int currentMP;
    public int CurrentMP { get => currentMP; set => currentMP = value; }

    protected override void Start()
    {
        base.Start();
        this.Init();
    }

    protected virtual void Init()
    {
        this.currentHP = this.maxHP;
        CurrentHPCtrl.Instance.UpdateCurrentHP(this.currentHP);
        this.currentMP = this.maxMP;
        MPSlotCtrl.Instance.UpdateCurrentMPSlot(this.currentMP);
    }

    public virtual void AddHP(int hp)
    {
        this.currentHP += hp;
        if (this.currentHP > this.maxHP)
            this.currentHP = this.maxHP;
        CurrentHPCtrl.Instance.UpdateCurrentHP(this.currentHP);
    }

    public virtual void SubHP(int damage)
    {
        this.currentHP -= damage;
        if (this.currentHP < 0)
            this.currentHP = 0;
        CurrentHPCtrl.Instance.UpdateCurrentHP(this.currentHP);
    }

    public virtual void AddMP(int mp)
    {
        if (this.currentMP == this.maxMP) return;
        this.currentMP += mp;
        if (this.currentMP > this.maxMP) this.currentMP = this.maxMP;
        MPSlotCtrl.Instance.UpdateCurrentMPSlot(this.currentMP);
    }

    public virtual void SubMP(int mp)
    {
        this.currentMP -= mp;
        MPSlotCtrl.Instance.UpdateCurrentMPSlot(this.currentMP);
    }

    public virtual bool UseMP(int mp)
    {
        if (this.currentMP < mp) return false;
        this.SubMP(mp);
        return true;
    }
}
