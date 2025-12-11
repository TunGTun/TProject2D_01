using UnityEngine;
using UnityEngine.Tilemaps;

public class RevealFogOnMinimap : MonoBehaviour
{
    public Tilemap fogTilemap;   // Tilemap fog
    public Transform player;     // Player transform
    public int revealRadius = 1; // 1 = 3x3, 2 = 5x5, 3 = 7x7

    void Update()
    {
        if (fogTilemap == null || player == null)
            return;

        // Lấy cell tilemap dưới chân player
        Vector3Int centerCell = fogTilemap.WorldToCell(player.position);

        // Xoá fog xung quanh theo radius
        for (int x = -revealRadius; x <= revealRadius; x++)
        {
            for (int y = -revealRadius; y <= revealRadius; y++)
            {
                Vector3Int cell = centerCell + new Vector3Int(x, y, 0);

                if (fogTilemap.HasTile(cell))
                {
                    fogTilemap.SetTile(cell, null);
                }
            }
        }
    }
}
