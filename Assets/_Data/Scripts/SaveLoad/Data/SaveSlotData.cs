using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveSlotData
{
    public PlayerData Player;
    public Dictionary<string, SceneData> Scenes = new();
}
