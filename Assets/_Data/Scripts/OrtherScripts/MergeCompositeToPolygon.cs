using System.Collections.Generic;
using UnityEngine;
using Clipper2Lib;

[RequireComponent(typeof(PolygonCollider2D))]
public class MergeCompositeToPolygon : MySingleton<MergeCompositeToPolygon>
{
    [Header("Source Objects (Auto load by Tag)")]
    [SerializeField] private List<CompositeCollider2D> sourceComposites = new List<CompositeCollider2D>();

    [Header("Target Polygon")]
    [SerializeField] private PolygonCollider2D targetPolygon;

    [Header("Merge Settings")]
    [SerializeField] private float scale = 1000f;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadTargetPolygon();
        this.LoadSourceComposites();

        this.MergeComposites();
    }

    protected virtual void LoadTargetPolygon()
    {
        if (this.targetPolygon != null) return;
        this.targetPolygon = GetComponent<PolygonCollider2D>();
        Debug.LogWarning(transform.name + ": LoadTargetPolygon", gameObject);
    }

    protected virtual void LoadSourceComposites()
    {
        if (this.sourceComposites.Count > 0) return;
        this.sourceComposites = new List<CompositeCollider2D>();

        CompositeCollider2D fixVoidRift = GameObject.Find("FixVoidRift").GetComponent<CompositeCollider2D>();
        this.sourceComposites.Add(fixVoidRift);

        string[] tags = { "Ground", "Wall", "Ceiling" };
        foreach (string tag in tags)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
            foreach (var obj in objs)
            {
                CompositeCollider2D comp = obj.GetComponent<CompositeCollider2D>();
                if (comp != null && !sourceComposites.Contains(comp))
                    sourceComposites.Add(comp);
            }
        }

        Debug.Log(transform.name + ": LoadSourceComposites", gameObject);

    }

    public virtual void MergeComposites()
    {
        PathsD subjectPaths = new PathsD();

        foreach (var comp in sourceComposites)
        {
            if (!comp) continue;
            int pathCount = comp.pathCount;

            for (int i = 0; i < pathCount; i++)
            {
                int pointCount = comp.GetPathPointCount(i);
                Vector2[] temp = new Vector2[pointCount];
                comp.GetPath(i, temp);

                PathD path = new PathD(pointCount);
                for (int j = 0; j < pointCount; j++)
                {
                    Vector2 worldP = comp.transform.TransformPoint(temp[j]);
                    path.Add(new PointD(worldP.x * scale, worldP.y * scale));
                }

                subjectPaths.Add(path);
            }
        }

        PathsD solution = Clipper.Union(subjectPaths, FillRule.NonZero);

        targetPolygon.pathCount = solution.Count;

        for (int i = 0; i < solution.Count; i++)
        {
            PathD path = solution[i];
            Vector2[] localPoints = new Vector2[path.Count];

            for (int j = 0; j < path.Count; j++)
            {
                Vector2 worldPoint = new Vector2((float)(path[j].x / scale), (float)(path[j].y / scale));
                localPoints[j] = transform.InverseTransformPoint(worldPoint);
            }

            targetPolygon.SetPath(i, localPoints);
        }
    }
}