using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/OreGG")]
public class OreGeo : Geo
{
    public TileData tile;
    public float Threshold;
    public float xMult;
    public float yMult;

    public override void Generate(Generation gen, dynamic param)
    {
        (int seed, int width, int height) Config = param;

        for (int x = 0; x < Config.width; x++)
            for (int y = 0; y < Config.height; y++)
            {
                float temp = Algorithms.Perlin(x, y, Config.seed, xMult, yMult);

                if (temp >= Threshold)
                    gen.tileMatrix[x, y].SetTileData(tile);
            }
    }
}
