using UnityEngine;

public abstract class BaseBossData : MyMonoBehaviour
{
    [Header("BaseBossData")]

    //public int CurrentHealth { get; set; } = 5;

    [SerializeField] protected int currentHealth = 5;
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
}
