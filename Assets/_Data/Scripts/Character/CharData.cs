using UnityEngine;

public class CharData : MyMonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int maxHP = 5; //toi da 15
    public int MaxHP 
    { 
        get => maxHP; 
        set 
        { 
            maxHP = value;
            HPSlotCtrl.Instance.UpdateMaxHP(this.MaxHP);
        } 
    }

    [SerializeField] protected int maxMP = 100; // toi da 300
    public int MaxMP 
    { 
        get => maxMP; 
        set 
        { 
            maxMP = value;
            MPSlotCtrl.Instance.UpdateMaxMP(this.MaxMP);
        } 
    }

    [SerializeField] protected int attackDamage = 10;
    public int AttackDamage { get => attackDamage; set => attackDamage = value; }

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

    [SerializeField] protected int currentMP;
    public int CurrentMP
    {
        get => currentMP;
        set
        {
            currentMP = value;
            MPSlotCtrl.Instance.UpdateCurrentMPSlot(this.currentMP);
        }
    }

    [Header("Economy")]
    [SerializeField] protected int money;
    public int Money
    { 
        get => money; 
        set 
        { 
            money = value;
            MoneyUICtrl.Instance.SetMoneyText(this.money);
        } 
    }

    protected override void Start()
    {
        base.Start();
        this.Init();
    }

    protected virtual void Init()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) this.AddMoney(100);
        if (Input.GetKeyDown(KeyCode.O)) this.UseMoney(1000);
    }

    //HP
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

    //MP
    public virtual void AddMP(int mp)
    {
        if (this.CurrentMP == this.maxMP) return;
        this.CurrentMP += mp;
        if (this.CurrentMP > this.maxMP) this.CurrentMP = this.maxMP;
    }

    public virtual void SubMP(int mp)
    {
        this.CurrentMP -= mp;
    }

    public virtual bool UseMP(int mp)
    {
        if (this.CurrentMP < mp) return false;
        this.SubMP(mp);
        return true;
    }

    //Money
    public virtual void AddMoney(int addMoney)
    {
        this.Money += addMoney;
    }

    public virtual void SubMoney(int subMoney)
    {
        this.Money -= subMoney;
    }

    public virtual bool UseMoney(int moneyNeed)
    {
        if (this.Money < moneyNeed) return false;
        this.SubMoney(moneyNeed);
        return true;
    }

    //MaxHP
    public virtual void AddMaxHP(int value)
    {
        this.MaxHP += value;
        if (this.MaxHP > 15)
            this.MaxHP = 15;
    }

    public virtual void SubMaxHP(int value)
    {
        this.MaxHP -= value;
        if (this.MaxHP < 0)
            this.MaxHP = 0;
    }

    //MaxMP
    public virtual void AddMaxMP(int value)
    {
        this.MaxMP += value;
        if (this.MaxMP > 300)
            this.MaxMP = 300;
    }

    public virtual void SubMaxMP(int value)
    {
        this.MaxMP -= value;
        if (this.MaxMP < 0)
            this.MaxMP = 0;
    }

    //Attack
    public virtual void AddAttackDamage(int value)
    {
        this.AttackDamage += value;
    }

    public virtual void SubAttackDamage(int value)
    {
        this.AttackDamage -= value;
        if (this.AttackDamage < 0)
            this.AttackDamage = 0;
    }
}
