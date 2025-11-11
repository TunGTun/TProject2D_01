using UnityEngine;

public class DontDestroyOnLoad : MyMonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
