using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossMinotaurControl : BaseBossControl
{
    protected BossMinotaurSkillOne skillOne;
    protected BossMinotaurSkillTwo skillTwo;
    protected BossMinotaurSkillThree skillThree;
    protected BossMinotaurSkillFour skillFour;

    private float skillCooldownTimer = 0f;
    private Transform player;

    [Header("Skill Ranges")]
    public float meleeRange = 3f;
    // public float midRange = 8f;
    public float farRange = 10f;

    // ----------------- Phase 1 Settings -----------------
    [System.Serializable]
    public class Phase1Settings
    {
        [Header("Delays (Cooldowns)")]
        public Vector2 delaySkill1 = new Vector2(0.5f, 0.8f);
        public Vector2 delaySkill2 = new Vector2(0.7f, 1.0f);
        public Vector2 delaySkill3 = new Vector2(2.0f, 2.5f);
        public Vector2 delaySkill4 = new Vector2(3.0f, 3.5f);

        [Header("Near (≤ meleeRange) %")]
        [Range(0,100)] public int nearSkill1 = 50;
        [Range(0,100)] public int nearSkill2 = 45;
        [Range(0,100)] public int nearSkill3 = 0;
        [Range(0,100)] public int nearSkill4 = 5;

        [Header("Mid (≤ midRange) %")]
        [Range(0,100)] public int midSkill1 = 25;
        [Range(0,100)] public int midSkill2 = 25;
        [Range(0,100)] public int midSkill3 = 30;
        [Range(0,100)] public int midSkill4 = 20;

        [Header("Far (> midRange) %")]
        [Range(0,100)] public int farSkill1 = 10;
        [Range(0,100)] public int farSkill2 = 10;
        [Range(0,100)] public int farSkill3 = 60;
        [Range(0,100)] public int farSkill4 = 20;
    }

    // ----------------- Phase 2 Settings -----------------
    [System.Serializable]
    public class Phase2Settings
    {
        [Header("Delays (Cooldowns)")]
        public Vector2 delaySkill1 = new Vector2(0.5f, 0.8f);
        public Vector2 delaySkill2 = new Vector2(0.7f, 1f);
        public Vector2 delaySkill3 = new Vector2(1.5f, 2.0f);
        public Vector2 delaySkill4 = new Vector2(2.0f, 2.5f);

        [Header("Near (≤ meleeRange) %")]
        [Range(0,100)] public int nearSkill1 = 35;
        [Range(0,100)] public int nearSkill2 = 35;
        [Range(0,100)] public int nearSkill3 = 20;
        [Range(0,100)] public int nearSkill4 = 10;

        [Header("Mid (≤ midRange) %")]
        [Range(0,100)] public int midSkill1 = 10;
        [Range(0,100)] public int midSkill2 = 10;
        [Range(0,100)] public int midSkill3 = 55;
        [Range(0,100)] public int midSkill4 = 25;

        [Header("Far (> midRange) %")]
        [Range(0,100)] public int farSkill1 = 0;
        [Range(0,100)] public int farSkill2 = 10;
        [Range(0,100)] public int farSkill3 = 60;
        [Range(0,100)] public int farSkill4 = 30;
    }

    [Header("Phase 1 Config")]
    public Phase1Settings phase1 = new Phase1Settings();

    [Header("Phase 2 Config")]
    public Phase2Settings phase2 = new Phase2Settings();

    // ===================================================
    protected override void Start()
    {
        base.Start();
        Init();
    }

    protected virtual void Init()
    {
        skillOne = new BossMinotaurSkillOne();
        skillTwo = new BossMinotaurSkillTwo();
        skillThree = new BossMinotaurSkillThree();
        skillFour = new BossMinotaurSkillFour();

        baseBossCtrl.BaseBossState.ChangeState(baseBossCtrl.BaseBossState.idle);
        player = baseBossCtrl.BossTarget.Target;
    }

    private void Update()
    {
        // Tick skills
        if (skillOne?.IsRunning() == true) skillOne.Tick(baseBossCtrl);
        if (skillTwo?.IsRunning() == true) skillTwo.Tick(baseBossCtrl);
        if (skillThree?.IsRunning() == true) skillThree.Tick(baseBossCtrl);
        if (skillFour?.IsRunning() == true) skillFour.Tick(baseBossCtrl);

        if (skillCooldownTimer > 0f)
        {
            skillCooldownTimer -= Time.deltaTime;
            return;
        }

        if (!IsExecutingSkill)
            ChooseAndExecuteSkill();
    }

    private void ChooseAndExecuteSkill()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        bool isPhase2 = baseBossCtrl.BaseBossData.CurrentHealth <= SBossMinotaurStaticData.MaxHP * 0.5f;

        if (isPhase2)
        {
            if (distance <= meleeRange) HandleRange(phase2, RangeType.Near);
            else if (distance <= farRange) HandleRange(phase2, RangeType.Mid);
            else HandleRange(phase2, RangeType.Far);
        }
        else
        {
            if (distance <= meleeRange) HandleRange(phase1, RangeType.Near);
            else if (distance <= farRange) HandleRange(phase1, RangeType.Mid);
            else HandleRange(phase1, RangeType.Far);
        }
    }

    private enum RangeType { Near, Mid, Far }

    // --- Phase1 ---
    private void HandleRange(Phase1Settings s, RangeType type)
    {
        int[] probs = GetProbs(s, type);
        int total = 0;
        for (int i = 0; i < probs.Length; i++) total += Mathf.Max(0, probs[i]);
        if (total <= 0) return;

        int roll = Random.Range(0, total);
        int cumulative = 0;

        for (int i = 0; i < probs.Length; i++)
        {
            cumulative += Mathf.Max(0, probs[i]);
            if (roll < cumulative)
            {
                ExecuteSkillByIndex(i + 1, s);
                return;
            }
        }
    }

    // --- Phase2 ---
    private void HandleRange(Phase2Settings s, RangeType type)
    {
        int[] probs = GetProbs(s, type);
        int total = 0;
        for (int i = 0; i < probs.Length; i++) total += Mathf.Max(0, probs[i]);
        if (total <= 0) return;

        int roll = Random.Range(0, total);
        int cumulative = 0;

        for (int i = 0; i < probs.Length; i++)
        {
            cumulative += Mathf.Max(0, probs[i]);
            if (roll < cumulative)
            {
                ExecuteSkillByIndex(i + 1, s);
                return;
            }
        }
    }

    // --- Generic Helper ---
    private int[] GetProbs(object settings, RangeType type)
    {
        if (settings is Phase1Settings s1)
        {
            return type switch
            {
                RangeType.Near => new int[] { s1.nearSkill1, s1.nearSkill2, s1.nearSkill3, s1.nearSkill4 },
                RangeType.Mid => new int[] { s1.midSkill1, s1.midSkill2, s1.midSkill3, s1.midSkill4 },
                _ => new int[] { s1.farSkill1, s1.farSkill2, s1.farSkill3, s1.farSkill4 }
            };
        }
        else if (settings is Phase2Settings s2)
        {
            return type switch
            {
                RangeType.Near => new int[] { s2.nearSkill1, s2.nearSkill2, s2.nearSkill3, s2.nearSkill4 },
                RangeType.Mid => new int[] { s2.midSkill1, s2.midSkill2, s2.midSkill3, s2.midSkill4 },
                _ => new int[] { s2.farSkill1, s2.farSkill2, s2.farSkill3, s2.farSkill4 }
            };
        }
        return new int[4];
    }

    private void ExecuteSkillByIndex(int index, Phase1Settings s)
    {
        ExecuteSkillByIndex(index,
            index switch
            {
                1 => Random.Range(s.delaySkill1.x, s.delaySkill1.y),
                2 => Random.Range(s.delaySkill2.x, s.delaySkill2.y),
                3 => Random.Range(s.delaySkill3.x, s.delaySkill3.y),
                _ => Random.Range(s.delaySkill4.x, s.delaySkill4.y)
            });
    }

    private void ExecuteSkillByIndex(int index, Phase2Settings s)
    {
        ExecuteSkillByIndex(index,
            index switch
            {
                1 => Random.Range(s.delaySkill1.x, s.delaySkill1.y),
                2 => Random.Range(s.delaySkill2.x, s.delaySkill2.y),
                3 => Random.Range(s.delaySkill3.x, s.delaySkill3.y),
                _ => Random.Range(s.delaySkill4.x, s.delaySkill4.y)
            });
    }

    private void ExecuteSkillByIndex(int index, float delay)
    {
        IBossSkill skill = index switch
        {
            1 => skillOne,
            2 => skillTwo,
            3 => skillThree,
            4 => skillFour,
            _ => null
        };

        if (skill == null) return;
        skill.Execute(baseBossCtrl);
        skillCooldownTimer = delay;
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying && player == null)
        {
            // Tìm player khi ở ngoài runtime (nếu muốn hiển thị tạm)
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);

        // Gizmos.color = Color.yellow;
        // Gizmos.DrawWireSphere(transform.position, midRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, farRange);

        // Vẽ đường nối đến player để dễ hình dung hướng
        if (player != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}

// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Serialization;
//
// public class BossMinotaurControl : BaseBossControl
// {
//     protected BossMinotaurSkillOne skillOne;
//     protected BossMinotaurSkillTwo skillTwo;
//     protected BossMinotaurSkillThree skillThree;
//     protected BossMinotaurSkillFour skillFour;
//     
//     private Transform player;
//
//     [Header("Skill 4 Settings")]
//     public LayerMask wallMask;
//     public float skillFourCheckDistance = 5f;
//
//     private readonly float[] hpThresholds = { 0.75f, 0.5f, 0.25f, 0.1f };
//     private readonly HashSet<float> usedThresholds = new HashSet<float>();
//     private bool pendingSkillFour = false;
//     
//     protected override void Start()
//     {
//         base.Start();
//         this.Init();
//     }
//
//     protected virtual void Init()
//     {
//         skillOne = new BossMinotaurSkillOne();
//         skillTwo = new BossMinotaurSkillTwo();
//         skillThree = new BossMinotaurSkillThree();
//         skillFour = new BossMinotaurSkillFour();
//
//         this.baseBossCtrl.BaseBossState.ChangeState(this.baseBossCtrl.BaseBossState.idle);
//         this.player = this.baseBossCtrl.BossTarget.Target;
//     }
//
//     private void Update()
//     {
//         if (skillOne != null && skillOne.IsRunning()) skillOne.Tick(this.baseBossCtrl);
//         if (skillTwo != null && skillTwo.IsRunning()) skillTwo.Tick(this.baseBossCtrl);
//         if (skillThree != null && skillThree.IsRunning()) skillThree.Tick(this.baseBossCtrl);
//         if (skillFour != null && skillFour.IsRunning()) skillFour.Tick(this.baseBossCtrl);
//         
//         CheckSkillFourTrigger();
//     }
//     
//     private void CheckSkillFourTrigger()
//     {
//         float hpPercent = (float)baseBossCtrl.BaseBossData.CurrentHealth / SBossMinotaurStaticData.MaxHP;
//
//         List<float> passedThresholds = new List<float>();
//         foreach (float t in hpThresholds)
//         {
//             if (!usedThresholds.Contains(t) && hpPercent <= t)
//                 passedThresholds.Add(t);
//         }
//
//         if (passedThresholds.Count > 0)
//         {
//             float chosenThreshold = Mathf.Max(passedThresholds.ToArray());
//             usedThresholds.Add(chosenThreshold);
//
//             TryExecuteSkillFour(chosenThreshold);
//         }
//
//         if (pendingSkillFour && !skillFour.IsRunning())
//         {
//             if (CanExecuteSkillFour())
//             {
//                 pendingSkillFour = false;
//                 skillFour.Execute(baseBossCtrl);
//             }
//         }
//     }
//     
//     private void TryExecuteSkillFour(float threshold)
//     {
//         if (CanExecuteSkillFour())
//         {
//             skillFour.Execute(baseBossCtrl);
//             pendingSkillFour = false;
//         }
//         else
//         {
//             pendingSkillFour = true;
//         }
//     }
//
//     private bool CanExecuteSkillFour()
//     {
//         Vector2 dir = Mathf.Approximately(baseBossCtrl.transform.localScale.x, 1) ? Vector2.right : Vector2.left;
//         RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, skillFourCheckDistance, wallMask);
//         return hit.collider == null;
//     }
//     
// }
