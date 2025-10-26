using UnityEngine;

public class CharData : MyMonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int maxHP = 5;
    public int MaxHP => maxHP;

    [SerializeField] protected int maxMP = 100;
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
        this.currentMP = this.maxMP;
    }

    public virtual void AddHP(int hp)
    {
        this.currentHP += hp;
        if (this.currentHP > this.maxHP)
            this.currentHP = this.maxHP;
    }

    public virtual void SubHP(int damage)
    {
        this.currentHP -= damage;
        if (this.currentHP < 0)
            this.currentHP = 0;
    }

    public virtual void AddMP(int mp)
    {
        if (this.currentMP == this.maxMP) return;
        this.currentMP += mp;
        if (this.currentMP > this.maxMP) this.currentMP = this.maxMP;
    }

    public virtual void SubMP(int mp)
    {
        this.currentMP -= mp;
    }

    public virtual bool UseMP(int mp)
    {
        if (this.currentMP < mp) return false;
        this.SubMP(mp);
        return true;
    }
}
