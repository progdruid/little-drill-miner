using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tile : MonoBehaviour
{
    //can be null
    public Tile upTile;
    public Tile rightTile;
    public Tile downTile;
    public Tile leftTile;

    private TileData tileData;

    public Tile (TileData _tileData)
    {
        tileData = _tileData;
    }

    public TileData GetTileData ()
    {
        return tileData;
    }

    public void SetTileData (TileData _tileData)
    {
        if (_tileData == null)
            return;

        tileData = _tileData;
        Sprite sprite = tileData.tileset[GetConnection()];
        UpdateSprite(sprite);

        upTile.Ping();
        rightTile.Ping();
        downTile.Ping();
        leftTile.Ping();
    }

    public void Ping ()
    {
        if (tileData == null)
            return;

        Sprite sprite = tileData.tileset[GetConnection()];
        UpdateSprite(sprite);
    }

    private int GetConnection()
    {
        bool up = false;
        bool right = false;
        bool down = false;
        bool left = false;
        try { up = tileData.dissonantTiles.Contains(upTile.GetTileData()); } catch { }
        try { right = tileData.dissonantTiles.Contains(rightTile.GetTileData()); } catch { }
        try { down = tileData.dissonantTiles.Contains(downTile.GetTileData()); } catch { }
        try { left = tileData.dissonantTiles.Contains(leftTile.GetTileData()); } catch { }

        int res = (up ? 1 : 0) + (right ? 1 : 0) * 2 + (down ? 1 : 0) * 4 + (left ? 1 : 0) * 8;
        return res;
    }

    private void UpdateSprite (Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
