using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "_Asset/SceneOffsetData", menuName = "Minimap/SceneOffsetData")]
public class SceneOffsetData : ScriptableObject
{
    [Serializable]
    public struct Entry
    {
        public string sceneName;
        public Vector3Int offset; // offset in minimap tile cells
    }

    public List<Entry> entries = new List<Entry>();

    // helper
    public Vector3Int GetOffsetForScene(string sceneName)
    {
        for (int i = 0; i < entries.Count; i++)
            if (entries[i].sceneName == sceneName) return entries[i].offset;
        return Vector3Int.zero;
    }
}
