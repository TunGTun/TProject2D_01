using UnityEngine;
using UnityEngine.Tilemaps;

public class RevealFogOnMinimap : MonoBehaviour
{
    public Tilemap fogTilemap;
    public Transform player;
    private Grid grid;

    void Start()
    {
        grid = fogTilemap.layoutGrid;
    }

    void Update()
    {
        if (fogTilemap == null || player == null)
            return;

        // Chuyển player từ worldPosition → localPosition của tilemap
        Vector3Int cellPos = fogTilemap.WorldToCell(player.position);

        // Xóa tile fog
        if (fogTilemap.HasTile(cellPos))
        {
            fogTilemap.SetTile(cellPos, null);
        }
    }
}
