using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent (typeof(Light2D))]

public class LightCtrl : MyMonoBehaviour
{
    [SerializeField] protected Light2D light2D;
    public Light2D Light2D => light2D;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLight2D();
    }

    protected virtual void LoadLight2D()
    {
        if (light2D != null) return;
        this.light2D = GetComponent<Light2D>();

        this.light2D.pointLightInnerRadius = 0.5f;
        this.light2D.pointLightOuterRadius = 5.5f;
        this.light2D.falloffIntensity = 0.75f;

        var layers = SortingLayer.layers;
        int[] allLayerIds = new int[layers.Length];
        for (int i = 0; i < layers.Length; i++)
            allLayerIds[i] = layers[i].id;
        var field = typeof(Light2D).GetField("m_ApplyToSortingLayers",
            BindingFlags.NonPublic | BindingFlags.Instance);
        if (field != null)
            field.SetValue(this.light2D, allLayerIds);

        Debug.Log(transform.name + ": LoadLight2D", gameObject);
    }
}
