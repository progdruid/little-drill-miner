using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/OreGG")]
public class OreGeoGen : IGeoGen
{
    private TileData[,] tileMatrix;
    private Generation gen;
    private int seed;

    public TileData tile;
    public float Rareness;
    public float xMult;
    public float yMult;

    public override TileData[,] GenGeo(Generation _generation, int _seed)
    {
        //init
        gen = _generation;
        seed = _seed;

        tileMatrix = new TileData[gen.maxPoint.x, gen.maxPoint.y];
        //tile = geo.tiles[0];

        //generation
        GenNoiseMatrix();
        return tileMatrix;
    }
    
    private void GenNoiseMatrix ()
    {
        for (int x = 0; x < gen.maxPoint.x; x++)
            for (int y = 0; y < gen.maxPoint.y; y++)
            {
                //
                float temp = Algorithms.Perlin(x, y, seed, xMult, yMult);

                if (temp >= Rareness)
                    tileMatrix[x, y] = tile;
            }
    }
}
