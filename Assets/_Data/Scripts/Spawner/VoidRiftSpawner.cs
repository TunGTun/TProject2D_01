using UnityEngine;

//Viết lại singleton
public class VoidRiftSpawner : ABaseSpawner
{
    private static VoidRiftSpawner instance;
    public static VoidRiftSpawner Instance { get => instance; }

    public string VoidRift = "VoidRift";

    public Transform CurrentRift;

    [SerializeField] protected float voidRiftOffset = 0.5f;

    [SerializeField] protected LayerMask environmentMask;

    protected override void Awake()
    {
        base.Awake();
        if (VoidRiftSpawner.instance != null) Debug.LogError("Only 1 VoidRiftSpawner allow to exist");
        VoidRiftSpawner.instance = this;

        environmentMask = LayerMask.GetMask("Ground", "Wall", "Ceiling");
    }

    public override Transform Spawn(Transform prefab, Vector3 spawnPos, Quaternion rotation)
    {
        if (CurrentRift != null)
        {
            this.Despawn(CurrentRift);
        }

        CurrentRift = base.Spawn(prefab, spawnPos, rotation);

        return CurrentRift;
    }

    public Transform Spawn(string prefabName, Vector3 spawnPos, Quaternion rotation, bool isHorizontal)
    {
        Collider2D envCollider = Physics2D.OverlapCircle(spawnPos, voidRiftOffset, environmentMask);
        
        if (envCollider == null) return this.Spawn(prefabName, spawnPos, rotation);

        if (envCollider is BoxCollider2D box)
        {
            Bounds b = box.bounds;
            Vector3 newPos = spawnPos;

            if (isHorizontal)
            {
                float distLeft = Mathf.Abs(spawnPos.x - b.min.x);
                float distRight = Mathf.Abs(spawnPos.x - b.max.x);
                newPos.x = distLeft < distRight ? b.min.x : b.max.x;
            }
            else
            {
                float distBottom = Mathf.Abs(spawnPos.y - b.min.y);
                float distTop = Mathf.Abs(spawnPos.y - b.max.y);
                newPos.y = distBottom < distTop ? b.min.y : b.max.y;
            }

            spawnPos = newPos;
        }
        else
        {
            Vector2 closest = envCollider.ClosestPoint(spawnPos);

            if (isHorizontal)
            {
                spawnPos.x = closest.x;
            }
            else
            {
                spawnPos.y = closest.y;
            }
        }

        if (isHorizontal)
            spawnPos.x += Mathf.Sign(spawnPos.x - envCollider.bounds.center.x) * voidRiftOffset;
        else
            spawnPos.y += Mathf.Sign(spawnPos.y - envCollider.bounds.center.y) * voidRiftOffset;

        return this.Spawn(prefabName, spawnPos, rotation);
    }

    public override void Despawn(Transform obj)
    {
        base.Despawn(obj);
        CurrentRift = null;
    }
}
