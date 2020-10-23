using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/MainGG")]
public class MainGeo : Geo
{
    public TileData MainTile;

    public override void Generate(Generation gen, int width, int height, int seed)
    {

        for (int x = 0; x < gen.maxPoint.x; x++)
            for (int y = 0; y < gen.maxPoint.y; y++)
            {
                gen.tileMatrix[x, y].SetTileData(MainTile);
            }

    }
}
