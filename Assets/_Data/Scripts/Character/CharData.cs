using UnityEngine;

public class CharData : MyMonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int maxHP = 5; //toi da 15
    public int MaxHP { get => maxHP; set => maxHP = value; }

    [SerializeField] protected int maxMP = 100; // toi da 300
    public int MaxMP { get => maxMP; set => maxMP = value; }

    [SerializeField] protected int attackDamage = 10;
    public int AttackDamage { get => attackDamage; set => attackDamage = value; }

    [Header("Current Stats")]
    [SerializeField] protected int currentHP;
    public int CurrentHP { get => currentHP; set => currentHP = value; }

    [SerializeField] protected int currentMP;
    public int CurrentMP { get => currentMP; set => currentMP = value; }

    [Header("Economy")]
    [SerializeField] protected int money;
    public int Money { get => money; set => money = value; }

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) this.AddMoney(100);
        if (Input.GetKeyDown(KeyCode.O)) this.UseMoney(1000);
    }

    //HP
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

    //MP
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

    //Money
    public virtual void AddMoney(int addMoney)
    {
        this.money += addMoney;
        MoneyUICtrl.Instance.SetMoneyText(this.money);
    }

    public virtual void SubMoney(int subMoney)
    {
        this.money -= subMoney;
        MoneyUICtrl.Instance.SetMoneyText(this.money);
    }

    public virtual bool UseMoney(int moneyNeed)
    {
        if (this.money < moneyNeed) return false;
        this.SubMoney(moneyNeed);
        return true;
    }
}
