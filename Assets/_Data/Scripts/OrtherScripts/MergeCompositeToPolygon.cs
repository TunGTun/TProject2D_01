using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class MergeCompositeToPolygon : MyMonoBehaviour
{
    [Header("Composite Colliders")]
    [SerializeField] private List<CompositeCollider2D> sourceComposites = new List<CompositeCollider2D>();

    [Header("Polygon Collider")]
    [SerializeField] private PolygonCollider2D targetPolygon;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSourceComposites();
        this.LoadTargetPolygon();
        this.MergeComposites();
    }

    protected virtual void LoadSourceComposites()
    {
        if (sourceComposites.Count > 0) return;
        CompositeCollider2D ground = GameObject.Find("Ground").GetComponent<CompositeCollider2D>();
        this.sourceComposites.Add(ground);
        CompositeCollider2D wall = GameObject.Find("Wall").GetComponent<CompositeCollider2D>();
        this.sourceComposites.Add(wall);
        CompositeCollider2D ceiling = GameObject.Find("Ceiling").GetComponent<CompositeCollider2D>();
        this.sourceComposites.Add(ceiling);
        Debug.Log(transform.name + ": LoadSourceComposites", gameObject);
    }

    protected virtual void LoadTargetPolygon()
    {
        if (this.targetPolygon != null) return;
        this.targetPolygon = GetComponent<PolygonCollider2D>();
        Debug.LogWarning(transform.name + ": LoadTargetPolygon", gameObject);
    }

    protected virtual void MergeComposites()
    {
        List<Vector2[]> allPaths = new List<Vector2[]>();

        foreach (var comp in sourceComposites)
        {
            if (!comp) continue;

            int pathCount = comp.pathCount;

            for (int i = 0; i < pathCount; i++)
            {
                int pointCount = comp.GetPathPointCount(i);
                List<Vector2> worldPoints = new List<Vector2>(pointCount);
                Vector2[] temp = new Vector2[pointCount];

                comp.GetPath(i, temp);

                for (int j = 0; j < pointCount; j++)
                    worldPoints.Add(comp.transform.TransformPoint(temp[j]));

                allPaths.Add(worldPoints.ToArray());
            }
        }

        targetPolygon.pathCount = allPaths.Count;

        for (int i = 0; i < allPaths.Count; i++)
        {
            List<Vector2> localPoints = new List<Vector2>(allPaths[i].Length);
            foreach (var p in allPaths[i])
                localPoints.Add(transform.InverseTransformPoint(p));

            targetPolygon.SetPath(i, localPoints.ToArray());
        }
    }
}
