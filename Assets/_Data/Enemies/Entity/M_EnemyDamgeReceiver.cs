using UnityEngine;

public class M_EnemyDamgeReceiver : ADamageReceiver
{
    [SerializeField]protected M_EnemyHealth enemyHealth;
    [SerializeField] protected M_Enemy enemy;

    [SerializeField] protected SpriteRenderer enemySpriteRenderer;

    [Header("Flash Effect")]
    [SerializeField] protected Material originalMat;
    [SerializeField] protected Material hitMat;

    [SerializeField] private float flashDuration = 0.1f;
    private bool isFlashing = false;
    private float flashTimer = 0f;

    protected override void Start()
    {
        base.Start();
        originalMat = this.enemySpriteRenderer.material;
    }

    public override void OnDamageReceived(int damage)
    {
        enemyHealth.SubHP(damage);
        this.Flash();
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

    private void Update()
    {
        this.HandleFlash();
    }

    protected virtual void Flash()
    {
        this.enemySpriteRenderer.material = this.hitMat;
        flashTimer = flashDuration;
        isFlashing = true;
    }

    protected virtual void HandleFlash()
    {
        if (!isFlashing) return;
        flashTimer -= Time.deltaTime;
        if (flashTimer <= 0f)
        {
            this.enemySpriteRenderer.material = this.originalMat;
            isFlashing = false;
        }
    }
}
