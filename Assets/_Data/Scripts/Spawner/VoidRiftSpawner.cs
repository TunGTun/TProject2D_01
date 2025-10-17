using UnityEngine;

//Viết lại singleton
public class VoidRiftSpawner : ABaseSpawner
{
    private static VoidRiftSpawner instance;
    public static VoidRiftSpawner Instance { get => instance; }

    public string VoidRift = "VoidRift";

    public Transform CurrentRift;

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
        //Kiem tra spawn ben trong hay ben ngoai
        bool isSpawnInCol = false;
        Collider2D envCol = Physics2D.OverlapPoint(spawnPos, environmentMask);
        if (envCol != null) isSpawnInCol = true;
        
        Collider2D envCollider = Physics2D.OverlapCircle(spawnPos, SCharStaticData.RiftOffset, environmentMask);
        if (envCollider == null)
            return this.Spawn(prefabName, spawnPos, rotation);

        Vector2 edgePoint = spawnPos;

        if (envCollider is PolygonCollider2D poly)
        {
            edgePoint = this.FindNearestEdgePoint(poly, spawnPos, isHorizontal);
        }
        else if (envCollider is BoxCollider2D box)
        {
            Bounds b = box.bounds;

            if (isHorizontal)
            {
                float distLeft = Mathf.Abs(spawnPos.x - b.min.x);
                float distRight = Mathf.Abs(spawnPos.x - b.max.x);
                edgePoint.x = (distLeft < distRight) ? b.min.x : b.max.x;
                edgePoint.y = spawnPos.y;
            }
            else
            {
                float distBottom = Mathf.Abs(spawnPos.y - b.min.y);
                float distTop = Mathf.Abs(spawnPos.y - b.max.y);
                edgePoint.y = (distBottom < distTop) ? b.min.y : b.max.y;
                edgePoint.x = spawnPos.x;
            }
        }
        else
        {
            Vector2 closest = envCollider.ClosestPoint(spawnPos);
            edgePoint = closest;
        }

        if (isHorizontal)
            if (isSpawnInCol) 
                spawnPos.x = edgePoint.x - Mathf.Sign(spawnPos.x - edgePoint.x) * SCharStaticData.RiftOffset;
            else
                spawnPos.x = edgePoint.x + Mathf.Sign(spawnPos.x - edgePoint.x) * SCharStaticData.RiftOffset;
        else
            if (isSpawnInCol)
                spawnPos.y = edgePoint.y - Mathf.Sign(spawnPos.y - edgePoint.y) * SCharStaticData.RiftOffset;
            else
                spawnPos.y = edgePoint.y + Mathf.Sign(spawnPos.y - edgePoint.y) * SCharStaticData.RiftOffset;

        return this.Spawn(prefabName, spawnPos, rotation);
    }

    public override void Despawn(Transform obj)
    {
        base.Despawn(obj);
        CurrentRift = null;
    }
    
    public Vector2 FindNearestEdgePoint(PolygonCollider2D poly, Vector2 point, bool isHorizontal)
    {
        Vector2 closest = point;
        float minDist = float.MaxValue;

        for (int p = 0; p < poly.pathCount; p++)
        {
            Vector2[] path = poly.GetPath(p);

            for (int i = 0; i < path.Length; i++)
            {
                Vector2 a = poly.transform.TransformPoint(path[i]);
                Vector2 b = poly.transform.TransformPoint(path[(i + 1) % path.Length]);

                if (isHorizontal)
                {
                    if ((a.y <= point.y && b.y >= point.y) || (a.y >= point.y && b.y <= point.y))
                    {
                        float t = (point.y - a.y) / (b.y - a.y);
                        float x = Mathf.Lerp(a.x, b.x, t);
                        float dist = Mathf.Abs(point.x - x);

                        if (dist < minDist)
                        {
                            minDist = dist;
                            closest = new Vector2(x, point.y);
                        }
                    }
                }
                else
                {
                    if ((a.x <= point.x && b.x >= point.x) || (a.x >= point.x && b.x <= point.x))
                    {
                        float t = (point.x - a.x) / (b.x - a.x);
                        float y = Mathf.Lerp(a.y, b.y, t);
                        float dist = Mathf.Abs(point.y - y);

                        if (dist < minDist)
                        {
                            minDist = dist;
                            closest = new Vector2(point.x, y);
                        }
                    }
                }
            }
        }

        return closest;
    }
}
