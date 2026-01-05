using UnityEngine;

public class M_EnemyDamgeReceiver : ADamageReceiver
{
    [SerializeField]protected M_EnemyHealth enemyHealth;
    [SerializeField] protected M_Enemy enemy;

    public override void OnDamageReceived(int damage)
    {
        enemyHealth.SubHP(damage);
        FacePlayerWhenDamaged();
    }

    private void FacePlayerWhenDamaged()
    {
        GameObject player = CharCtrl.Instance.gameObject;
        if (player == null) return;

        float dir = player.transform.position.x - enemy.transform.position.x;


        if (dir < 0 && enemy.facingRight)
            enemy.Flip();

        // Nếu player bên phải, còn quái đang nhìn trái → quay lại
        else if (dir > 0 && !enemy.facingRight)
            enemy.Flip();
    }


}
