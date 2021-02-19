using UnityEngine;
using DruidLib;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/OreGG")]
public class OreGeo : Geo
{
    public TileData tile;
    public float Threshold;
    public float xMult;
    public float yMult;

    public override void Generate(Map map, Dict<string> Params)
    {
        int seed = (int)Params.GetData("Seed");

        TileData[,] layer = new TileData[map.width, map.height];

        for (int x = 0; x < map.width; x++)
            for (int y = 0; y < map.height; y++)
            {
                float temp = Algorithms.Perlin(x, y, seed, xMult, yMult);

                if (temp >= Threshold)
                    layer[x, y] = tile;
            }

        map.AddLayer(layer);
    }
}
