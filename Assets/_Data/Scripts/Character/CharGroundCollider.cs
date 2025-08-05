using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CharGroundCollider : MyMonoBehaviour
{
    [SerializeField] protected BoxCollider2D charGroundBoxCollider2D;
    public BoxCollider2D CharGroundBoxCollider2D => charGroundBoxCollider2D;
    protected override void LoadComponents()
    {
        base.LoadComponents(); ;
        this.LoadCharGroundBoxCollider2D();
    }
    protected virtual void LoadCharGroundBoxCollider2D()
    {
        if (charGroundBoxCollider2D != null) return;
        this.charGroundBoxCollider2D = GetComponent<BoxCollider2D>();
        this.charGroundBoxCollider2D.offset = new Vector2(0.01713603f, -1.024423f);        // Vị trí offset của collider so với tâm object
        this.charGroundBoxCollider2D.size = new Vector2(0.264516f, 0.0596776f);          // Kích thước collider
        this.charGroundBoxCollider2D.isTrigger = false;                                  // Bật nếu collider là trigger (không va chạm thật)
        Debug.Log(transform.name + ": LoadBoxCollider2D", gameObject);
    }
}
