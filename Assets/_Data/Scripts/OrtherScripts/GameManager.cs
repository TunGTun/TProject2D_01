using UnityEngine;

public class GameManager : MyMonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    protected override void Awake()
    {
        base.Awake();

        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("Duplicate GameManager detected, destroying the new one.");
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
