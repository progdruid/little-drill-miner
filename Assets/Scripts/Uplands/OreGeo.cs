using UnityEngine;
using DruidLib;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/OreGG")]
public class OreGeo : Geo
{
    public TileData tile;
    public float Threshold;
    public float xMult;
    public float yMult;

    public int seedIndex;

    public override void Generate(Map map, Dict<string> Params)
    {
        int seed = (int)Params.GetData("Seed") + seedIndex;

        TileData[,] layer = new TileData[map.width, map.height];

        for (int x = 0; x < map.width; x++)
            for (int y = 0; y < map.height; y++)
            {
                float perlin = Algorithms.PerlinNoise(x / xMult, y / yMult, seed);

                perlin *= perlin * 100;

                if (perlin >= Threshold)
                    layer[x, y] = tile;
            }

        map.AddLayer(layer);
    }
}
