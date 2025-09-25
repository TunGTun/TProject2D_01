using UnityEngine;

[CreateAssetMenu(menuName = "Boss/BossStaticData")]
public class BossStaticDataSO : ScriptableObject
{
    public int MaxHP;
    public float MoveSpeed;
    public int Damage;
    public float StopDistance;
}
