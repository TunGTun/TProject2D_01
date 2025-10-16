using System;
using UnityEngine;

public abstract class AAutoDespawnWithTime : MyMonoBehaviour
{
    [SerializeField] protected float DespawnTime = 0f;
    private float timer = 0f;

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= this.DespawnTime)
        {
            this.Despawn();
        }
    }

    protected abstract void Despawn();
}
