using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class AAutoDespawnWithTime : MyMonoBehaviour
{
    [SerializeField] protected float despawnTime = 0f;
    private float timer = 0f;

    protected override void OnEnable()
    {
        base.OnEnable();
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= this.despawnTime)
        {
            this.Despawn();
        }
    }

    protected abstract void Despawn();
}
