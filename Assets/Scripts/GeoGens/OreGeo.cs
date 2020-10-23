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

    public override void Generate(Generation gen, int width, int height, int seed)
    {

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                float temp = Algorithms.Perlin(x, y, seed, xMult, yMult);

                if (temp >= Threshold)
                    gen.tileMatrix[x, y].SetTileData(tile);
            }
    }
}
