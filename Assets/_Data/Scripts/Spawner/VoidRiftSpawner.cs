using UnityEngine;

//Viết lại singleton
public class VoidRiftSpawner : ABaseSpawner
{
    private static VoidRiftSpawner instance;
    public static VoidRiftSpawner Instance { get => instance; }

    //public static string bulletOne = "Bullet_1";

    public Transform currentRift;

    protected override void Awake()
    {
        base.Awake();
        if (VoidRiftSpawner.instance != null) Debug.LogError("Only 1 VoidRiftSpawner allow to exist");
        VoidRiftSpawner.instance = this;
    }

    public override Transform Spawn(string prefabName, Vector3 spawnPos, Quaternion rotation)
    {
        if (currentRift != null)
        {
            this.Despawn(currentRift);
        }

        currentRift = base.Spawn(prefabName, spawnPos, rotation);
        return currentRift;
    }

    public override Transform Spawn(Transform prefab, Vector3 spawnPos, Quaternion rotation)
    {
        if (currentRift != null)
        {
            this.Despawn(currentRift);
        }

        currentRift = base.Spawn(prefab, spawnPos, rotation);
        return currentRift;
    }

    public override void Despawn(Transform obj)
    {
        base.Despawn(obj);
        currentRift = null;
    }
}
