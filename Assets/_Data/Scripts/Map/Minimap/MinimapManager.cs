using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MinimapManager : MyMonoBehaviour
{
    public static MinimapManager Instance { get; private set; }

    [Header("Minimap Tilemaps (World Map)")]
    public Tilemap minimapTilemap;
    public Tilemap fogTilemap;

    [Header("Scene Offset Config")]
    public SceneOffsetData offsetData;

    [Header("Camera/UI")]
    public Camera minimapCamera;

    public void ApplySavedReveals()
    {
        if (fogTilemap == null) return;

        foreach (var c in MinimapMemory.revealedWorldCells)
        {
            fogTilemap.SetTile(c, null);
        }
    }

    public Vector3Int GetSceneOffset(string sceneName)
    {
        return offsetData != null ? offsetData.GetOffsetForScene(sceneName) : Vector3Int.zero;
    }

    public Vector3Int WorldPositionToWorldCell(Vector3 worldPos)
    {
        return minimapTilemap.WorldToCell(worldPos);
    }

}
