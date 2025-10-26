using UnityEngine;

public class LoadGameData : MyMonoBehaviour
{
    protected override void Start()
    {
        base.Start();
        SaveLoadManager.Instance.LoadGame();
    }
}
