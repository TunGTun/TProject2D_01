using System.Collections;
using UnityEngine;

public class LoadGameData : MyMonoBehaviour
{
    protected override void Start()
    {
        base.Start();
        this.LoadGame();
    }

    public virtual void LoadGame()
    {
        SaveLoadManager.Instance.LoadPlayer();
    }
}
