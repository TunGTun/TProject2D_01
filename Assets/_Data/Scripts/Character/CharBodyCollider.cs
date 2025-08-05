using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class CharBodyCollider : MyMonoBehaviour
{
    [SerializeField] protected BoxCollider2D charBodyBoxCollider2D;
    public BoxCollider2D CharBodyBoxCollider2D => charBodyBoxCollider2D;
    protected override void LoadComponents()
    {
        base.LoadComponents(); ;
        this.LoadCharBodyBoxCollider2D();
    }
    protected virtual void LoadCharBodyBoxCollider2D()
    {
        if (charBodyBoxCollider2D != null) return;
        this.charBodyBoxCollider2D = GetComponent<BoxCollider2D>();
        this.charBodyBoxCollider2D.offset = new Vector2(0.01792541f, -0.4775002f);        // Vị trí offset của collider so với tâm object
        this.charBodyBoxCollider2D.size = new Vector2(0.3403783f, 0.9620636f);          // Kích thước collider
        this.charBodyBoxCollider2D.isTrigger = false;                                  // Bật nếu collider là trigger (không va chạm thật)
        Debug.Log(transform.name + ": LoadBoxCollider2D", gameObject);
    }
}
