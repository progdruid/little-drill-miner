using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/MainGG")]
public class MainGeo : Geo
{
    public TileData MainTile;

    private int width, height;

    public override void Generate(Generation gen, params object[] _params)
    {
        width = (int)_params[1];
        height = (int)_params[2];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                gen.tileMatrix[x, y].SetTileData(MainTile);
            }

    }
}
