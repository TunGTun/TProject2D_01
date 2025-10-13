using System;
using UnityEngine;

public class AutoDespawnWithTime : MonoBehaviour
{
    private float timer = 0f;

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= SCharStaticData.RiftDespawnTime)
        {
            this.Despawn();
        }
    }

    protected virtual void Despawn()
    {
        VoidRiftSpawner.Instance.Despawn(VoidRiftSpawner.Instance.CurrentRift);
    }
}
