using UnityEngine;

public class BossMinotaurControl : BaseBossControl
{
    protected BossMinotaurSkillOne skillOne;
    protected BossMinotaurSkillTwo skillTwo;
    protected BossMinotaurSkillThree skillThree;
    protected BossMinotaurSkillFour skillFour;

    private float skillCooldownTimer = 0f;
    private Transform player;

    [Header("Skill Ranges")]
    public float meleeRange = 3f;    // Skill1,2
    public float midRange = 8f;      // Skill3
    public float farRange = 12f;     // Skill4

    // ----------------- Phase 1 Settings -----------------
    [System.Serializable]
    public class Phase1Settings
    {
        [Header("Delays")]
        public Vector2 delaySkill1 = new Vector2(0.3f, 0.5f);
        public Vector2 delaySkill2 = new Vector2(0.5f, 0.8f);
        public Vector2 delaySkill3 = new Vector2(0.8f, 1.0f);
        public Vector2 delaySkill4 = new Vector2(1.5f, 2.0f);

        [Header("Near (<= meleeRange) %")]
        public int nearSkill1 = 60;
        public int nearSkill2 = 30;
        public int nearSkill3 = 10;

        [Header("Mid (<= midRange) %")]
        public int midSkill3 = 50;
        public int midSkill1 = 30;
        public int midSkill2 = 20;

        [Header("Far (> midRange) %")]
        public int farSkill4 = 60;
        public int farSkill3 = 40;
    }
    public Phase1Settings phase1 = new Phase1Settings();

    // ----------------- Phase 2 Settings -----------------
    [System.Serializable]
    public class Phase2Settings
    {
        [Header("Delays")]
        public Vector2 delaySkill1 = new Vector2(0.2f, 0.4f);
        public Vector2 delaySkill2 = new Vector2(0.4f, 0.6f);
        public Vector2 delaySkill3 = new Vector2(0.6f, 0.8f);
        public Vector2 delaySkill4 = new Vector2(1.0f, 1.5f);

        [Header("Near (<= meleeRange) %")]
        public int nearSkill1 = 50;
        public int nearSkill2 = 30;
        public int nearSkill3 = 20;

        [Header("Mid (<= midRange) %")]
        public int midSkill3 = 40;
        public int midSkill1 = 40;
        public int midSkill2 = 20;

        [Header("Far (> midRange) %")]
        public int farSkill4 = 70;
        public int farSkill3 = 30;
    }
    public Phase2Settings phase2 = new Phase2Settings();

    // ===================================================

    protected override void Start()
    {
        base.Start();
        this.Init();
    }

    protected virtual void Init()
    {
        skillOne = new BossMinotaurSkillOne();
        skillTwo = new BossMinotaurSkillTwo();
        skillThree = new BossMinotaurSkillThree();
        skillFour = new BossMinotaurSkillFour();

        this.baseBossCtrl.BaseBossState.ChangeState(this.baseBossCtrl.BaseBossState.idle);
        this.player = this.baseBossCtrl.BossTarget.Target;
    }

    private void Update()
    {
        // Tick skill đang chạy
        if (skillOne != null && skillOne.IsRunning()) skillOne.Tick(this.baseBossCtrl);
        if (skillTwo != null && skillTwo.IsRunning()) skillTwo.Tick(this.baseBossCtrl);
        if (skillThree != null && skillThree.IsRunning()) skillThree.Tick(this.baseBossCtrl);
        if (skillFour != null && skillFour.IsRunning()) skillFour.Tick(this.baseBossCtrl);

        // Nếu đang cooldown thì đếm ngược
        if (skillCooldownTimer > 0f)
        {
            skillCooldownTimer -= Time.deltaTime;
            return;
        }

        //Nếu không có skill nào chạy → chọn skill tiếp
        if (!this.IsExecutingSkill)
        {
            ChooseAndExecuteSkill();
        }
    }

    private void ChooseAndExecuteSkill()
    {
        if (player == null) return;

        float distance = Vector2.Distance(this.transform.position, player.position);
        bool isPhase2 = this.baseBossCtrl.BaseBossData.CurrentHealth <= SBossMinotaurStaticData.MaxHP * 0.5f;

        if (distance <= meleeRange)
        {
            if (isPhase2) HandleNearRange(phase2);
            else HandleNearRange(phase1);
        }
        else if (distance <= midRange)
        {
            if (isPhase2) HandleMidRange(phase2);
            else HandleMidRange(phase1);
        }
        else
        {
            if (isPhase2) HandleFarRange(phase2);
            else HandleFarRange(phase1);
        }
    }

    // ----------------- Phase Handlers -----------------
    private void HandleNearRange(Phase1Settings s)
    {
        int roll = Random.Range(0, 100);

        if (roll < s.nearSkill1)
            ExecuteSkill(skillOne, Random.Range(s.delaySkill1.x, s.delaySkill1.y));
        else if (roll < s.nearSkill1 + s.nearSkill2)
            ExecuteSkill(skillTwo, Random.Range(s.delaySkill2.x, s.delaySkill2.y));
        else
            ExecuteSkill(skillThree, Random.Range(s.delaySkill3.x, s.delaySkill3.y));
    }

    private void HandleNearRange(Phase2Settings s)
    {
        int roll = Random.Range(0, 100);

        if (roll < s.nearSkill1)
            ExecuteSkill(skillOne, Random.Range(s.delaySkill1.x, s.delaySkill1.y));
        else if (roll < s.nearSkill1 + s.nearSkill2)
            ExecuteSkill(skillTwo, Random.Range(s.delaySkill2.x, s.delaySkill2.y));
        else
            ExecuteSkill(skillThree, Random.Range(s.delaySkill3.x, s.delaySkill3.y));
    }

    private void HandleMidRange(Phase1Settings s)
    {
        int roll = Random.Range(0, 100);

        if (roll < s.midSkill3)
            ExecuteSkill(skillThree, Random.Range(s.delaySkill3.x, s.delaySkill3.y));
        else if (roll < s.midSkill3 + s.midSkill1)
            ExecuteSkill(skillOne, Random.Range(s.delaySkill1.x, s.delaySkill1.y));
        else
            ExecuteSkill(skillTwo, Random.Range(s.delaySkill2.x, s.delaySkill2.y));
    }

    private void HandleMidRange(Phase2Settings s)
    {
        int roll = Random.Range(0, 100);

        if (roll < s.midSkill3)
            ExecuteSkill(skillThree, Random.Range(s.delaySkill3.x, s.delaySkill3.y));
        else if (roll < s.midSkill3 + s.midSkill1)
            ExecuteSkill(skillOne, Random.Range(s.delaySkill1.x, s.delaySkill1.y));
        else
            ExecuteSkill(skillTwo, Random.Range(s.delaySkill2.x, s.delaySkill2.y));
    }

    private void HandleFarRange(Phase1Settings s)
    {
        int roll = Random.Range(0, 100);

        if (roll < s.farSkill4)
            ExecuteSkill(skillFour, Random.Range(s.delaySkill4.x, s.delaySkill4.y));
        else
            ExecuteSkill(skillThree, Random.Range(s.delaySkill3.x, s.delaySkill3.y));
    }

    private void HandleFarRange(Phase2Settings s)
    {
        int roll = Random.Range(0, 100);

        if (roll < s.farSkill4)
            ExecuteSkill(skillFour, Random.Range(s.delaySkill4.x, s.delaySkill4.y));
        else
            ExecuteSkill(skillThree, Random.Range(s.delaySkill3.x, s.delaySkill3.y));
    }

    // ----------------- Wrapper -----------------
    private void ExecuteSkill(IBossSkill skill, float cooldown)
    {
        if (skill == null) return;

        skill.Execute(this.baseBossCtrl);
        skillCooldownTimer = cooldown;
    }
}
