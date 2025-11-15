using UnityEngine;

public class DontDestroyOnLoadSingleton : MySingleton<DontDestroyOnLoadSingleton>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
