using UnityEngine;

public abstract class MySingleton<T> : MyMonoBehaviour where T : MyMonoBehaviour
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();
                if (instance == null)
                    Debug.LogWarning($"No instance of {typeof(T)} found in scene!");
            }
            return instance;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        if (instance != null && instance != this)
        {
            Debug.LogWarning($"Duplicate singleton detected: {typeof(T)} on {gameObject.name}, destroying this one.");
            Destroy(gameObject);
            return;
        }

        instance = (T)(MyMonoBehaviour)this;
    }
}