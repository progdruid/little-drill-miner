using DruidLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/Amber")]
public class AmberGeo : Geo
{
    public float Radius;

    public Geo structGeo;
    public TileData tile;

    public override void Generate(Map map, Dict<string> Params)
    {
        int centerX = (int)Params.GetData("X");
        int centerY = (int)Params.GetData("Y");

        TileData[,] layer = new TileData[map.width, map.height];

        int x_start = Mathf.Clamp((int)(centerX - Radius), 0, map.width);
        int x_end = Mathf.Clamp((int)(centerX + Radius), 0, map.width) + 1;
        int y_start = Mathf.Clamp((int)(centerY - Radius), 0, map.height);
        int y_end = Mathf.Clamp((int)(centerY + Radius), 0, map.height) + 1;

        for (int x = x_start; x < x_end; x++)
            for (int y = y_start; y < y_end; y++)
            {
                int xdiff = centerX - x;
                int ydiff = centerY - y;
                float dist = Mathf.Sqrt(xdiff * xdiff + ydiff * ydiff);

                if (dist <= Radius)
                    layer[x, y] = tile;
            }

        map.AddLayer(layer);

        structGeo.Generate(map, Params);
    }
}
