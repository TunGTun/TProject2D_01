using UnityEngine;

public class M_EnemyDamgeReceiver : ADamageReceiver
{
    [SerializeField]protected M_EnemyHealth enemyHealth;

    public override void OnDamageReceived(int damage)
    {
        enemyHealth.SubHP(damage);
    }

    
}
