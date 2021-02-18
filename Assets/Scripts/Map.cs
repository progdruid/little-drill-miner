using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int width { get; private set; }
    public int height { get; private set; }

    private Tile[,] tilemap;

    public Map (Tile[,] _tilemap, int _width, int _height)
    {
        tilemap = _tilemap;
        width = _width;
        height = _height;
    }

    public void AddLayer (TileData[,] layer)
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                TileData tile = layer[x, y];
                tilemap[x, y].SetTileData(tile);
            }
    }

    public TileData GetTileAt (int x, int y)
    {
        if (x < 0 || x >= width)
            throw new System.Exception($"X was: {x}, but the width is: {width}");
        if (y < 0 || y >= height)
            throw new System.Exception($"Y was: {y}, but the height is: {height}");

        TileData tile = tilemap[x, y].GetTileData();

        return tile;
    }
}
