using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Bắt buộc để dùng sự kiện SceneLoaded

[RequireComponent(typeof(CapsuleCollider2D))]
public class CharDamageReceiver : MyMonoBehaviour
{
    [Header("CharDamageReceiver")]
    [SerializeField] protected CharCtrl charCtrl;
    public CharCtrl CharCtrl => charCtrl;

    [SerializeField] protected CapsuleCollider2D hitBoxCollider;
    public CapsuleCollider2D HitBoxCollider => hitBoxCollider;

    [SerializeField] protected bool canTakeDamage = true;
    public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }

    [Header("Knockback")]
    [SerializeField] protected float knockbackForce = 5f;
    [SerializeField] protected float knockbackUpForce = 2f;

    [Header("Dead")]
    [SerializeField] protected bool isDead;

    // Cờ đánh dấu đang trong quá trình hồi sinh
    private bool isRespawning = false;

    // --- 1. ĐĂNG KÝ SỰ KIỆN LOAD SCENE ---
    // Vì nhân vật là Singleton (DontDestroyOnLoad), Awake/Start không chạy lại khi qua màn.
    // Ta phải dùng sự kiện này để biết khi nào Scene mới đã tải xong để đặt vị trí.
    protected void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Hàm này tự động chạy khi MySceneManager tải xong Scene mới
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Chỉ thực hiện logic nếu đang trong quá trình hồi sinh
        if (this.isRespawning)
        {
            this.RespawnAtCheckpoint();
        }
    }
    // ------------------------------------

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharCtrl();
        this.LoadHitBoxCollider();
    }

    protected virtual void LoadCharCtrl()
    {
        if (charCtrl != null) return;
        charCtrl = GetComponentInParent<CharCtrl>();
    }

    protected virtual void LoadHitBoxCollider()
    {
        if (hitBoxCollider != null) return;
        this.hitBoxCollider = GetComponent<CapsuleCollider2D>();
        this.hitBoxCollider.offset = Vector2.zero;
        this.hitBoxCollider.size = new Vector2(0.5f, 1.12f);
        this.hitBoxCollider.isTrigger = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canTakeDamage) return;

        // Logic nhận damage
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy1"))
            this.OnDamageReceived(1, collision.transform);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy2"))
            this.OnDamageReceived(2, collision.transform);
    }

    protected virtual void OnDamageReceived(int damage, Transform attacker)
    {
        if (this.isDead) return; // Tránh nhận damage khi đã chết

        this.charCtrl.CharData.SubHP(damage);
        this.charCtrl.CharStateCtrl.StatusState.ChangeState(this.charCtrl.CharStateCtrl.StatusState.hurt);
        this.Knockback(attacker);

        if (this.CheckIsDead())
        {
            this.OnDead();
        }
    }

    public virtual void Knockback(Transform attacker)
    {
        Vector2 direction = (transform.position - attacker.position).normalized;
        Vector2 knockback = new Vector2(direction.x * knockbackForce, knockbackUpForce);

        this.charCtrl.RigidBody2D.linearVelocity = Vector2.zero;
        this.charCtrl.RigidBody2D.AddForce(knockback, ForceMode2D.Impulse);
    }

    protected virtual bool CheckIsDead()
    {
        this.isDead = (this.charCtrl.CharData.CurrentHP <= 0);
        return this.isDead;
    }

    protected virtual void OnDead()
    {
        if (this.isRespawning) return; // Tránh gọi trùng lặp
        StartCoroutine(OnDeadRoutine());
    }

    protected virtual IEnumerator OnDeadRoutine()
    {
        // 1. Đảm bảo thời gian chạy bình thường để Animation Dead chạy
        Time.timeScale = 1f;

        this.charCtrl.CharStateCtrl.StatusState.ChangeState(this.charCtrl.CharStateCtrl.StatusState.dead);

        // Chờ animation chết hoặc hiệu ứng
        yield return new WaitForSeconds(1f);

        // Hiển thị màn hình Game Over
        GamePanelCtrl.Instance.EnableDeadPanel();

        // Chờ người chơi xem màn hình Game Over (3s)
        yield return new WaitForSeconds(3f);
        GamePanelCtrl.Instance.DisableDeadPanel();

        // 2. Chuẩn bị dữ liệu để hồi sinh
        CheckPointData checkPointData = SaveLoadManager.Instance.PlayerData.LastCheckPoint;
        this.charCtrl.CharData.AddHP(this.charCtrl.CharData.MaxHP); // Hồi đầy máu

        // 3. Bật cờ hồi sinh (QUAN TRỌNG)
        // Để khi Scene mới load xong, hàm OnSceneLoaded biết cần phải dịch chuyển nhân vật
        this.isRespawning = true;
        this.isDead = false;

        // 4. Gọi MySceneManager để load scene (có hiệu ứng chuyển cảnh)
        string sceneToLoad = MySceneManager.Instance.GetCurrentSceneName();
        if (checkPointData != null && !string.IsNullOrEmpty(checkPointData.SceneName))
        {
            sceneToLoad = checkPointData.SceneName;
        }

        // Gọi hàm LoadScene của MySceneManager
        MySceneManager.Instance.LoadScene(sceneToLoad);

        // Kết thúc Coroutine tại đây. 
        // Việc đặt vị trí nhân vật sẽ do sự kiện OnSceneLoaded xử lý.
    }

    // Hàm thực hiện dịch chuyển nhân vật về Checkpoint
    protected virtual void RespawnAtCheckpoint()
    {
        CheckPointData checkPointData = SaveLoadManager.Instance.PlayerData.LastCheckPoint;

        // Kiểm tra xem Scene hiện tại có đúng là Scene của Checkpoint không
        if (checkPointData != null && MySceneManager.Instance.GetCurrentSceneName() == checkPointData.SceneName)
        {
            // Tắt vật lý tạm thời để teleport an toàn (tránh kẹt tường/rơi tự do)
            this.charCtrl.RigidBody2D.linearVelocity = Vector2.zero;
            this.charCtrl.RigidBody2D.simulated = false;

            // Set vị trí (Set vào CharCtrl vì CharDamageReceiver là con)
            this.charCtrl.transform.position = checkPointData.SpawnPoint;
            Debug.Log("Respawned Player at: " + checkPointData.SpawnPoint);

            // Bật lại vật lý
            this.charCtrl.RigidBody2D.simulated = true;

            // Đặt trạng thái về Spawn hoặc Idle
            this.charCtrl.CharStateCtrl.StatusState.ChangeState(this.charCtrl.CharStateCtrl.StatusState.spawn);

            // Lưu lại Player để đồng bộ HP
            SaveLoadManager.Instance.SavePlayer();
        }

        // Tắt cờ hồi sinh sau khi đã xử lý xong
        this.isRespawning = false;
    }
}