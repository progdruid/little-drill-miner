using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DruidLib;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/MainGG")]
public class MainGeo : Geo
{
    public TileData MainTile;

    public override void Generate(Map map, Dict<string> Params)
    {
        TileData[,] layer = new TileData[map.width, map.height];

        for (int x = 0; x < map.width; x++)
            for (int y = 0; y < map.height; y++)
            {
                layer[x, y] = MainTile;
            }

        map.AddLayer(layer);
    }
}
