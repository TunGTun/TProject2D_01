using UnityEngine;
using UnityEngine.Tilemaps;

public class RevealFogOnMinimap : MonoBehaviour
{
    public Tilemap fogTilemap;   // Tilemap fog
    public Transform player;     // Player transform
    public int revealX = 9;
    public int revealY = 5;

    void Update()
    {
        if (fogTilemap == null || player == null)
            return;

        // Lấy cell tilemap dưới chân player
        Vector3Int centerCell = fogTilemap.WorldToCell(player.position);

        // Xoá fog xung quanh theo radius
        for (int x = -revealX; x <= revealX; x++)
        {
            for (int y = -revealY; y <= revealY; y++)
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
