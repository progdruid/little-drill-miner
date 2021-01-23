using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/MainGG")]
public class MainGeo : Geo
{
    public TileData MainTile;

    private int width, height;

    public override void Generate(Generation gen, dynamic param)
    {
        (int seed, int width, int height) Config = param;

        for (int x = 0; x < Config.width; x++)
            for (int y = 0; y < Config.height; y++)
            {
                gen.tileMatrix[x, y].SetTileData(MainTile);
            }

    }
}
