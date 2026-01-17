using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string SceneName;
    public Vector3 Position;

    //Stats
    public int MaxHP;
    public int MaxMP;
    public int AttackDamage;

    //Current Stats
    public int CurrentHP;
    public int CurrentMP;

    //SkillUnlock
    //public HashSet<ESkill> UnlockedSkills;

    //Economy
    public int Money;

    //CheckPoint
    public CheckPointData LastCheckPoint;
}

