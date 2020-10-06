using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/CaveGG")]
public class CaveGG : IGeoGen
{
    //public
    public TileData tile;
    [Range(0, 100)]
    public double threshold;
    public int automataIters;
    public int rule;

    //private
    Generation gen;
    System.Random random;
    TileData[,] tileMatrix;
    int width, height;

    public override TileData[,] GenGeo(Generation generation, int seed)
    {
        //init
        gen = generation;
        random = new System.Random(seed);
        width = gen.maxPoint.x;
        height = gen.maxPoint.y;
        tileMatrix = new TileData[width, height];

        //gen
        bool[,] automata = new bool[width, height];
        CreateRandomPoints(automata, width, height);

        for (int i = 0; i < automataIters; i++)
            Algorithms.CellAutomataTurn(ref automata, width, height, ConditionFunc);

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                if (automata[x, y])
                    tileMatrix[x, y] = tile;
            }

        return tileMatrix;
    }

    private bool ConditionFunc (int count)
    {
        return count >= rule;
    }

    private void CreateRandomPoints (bool[,] matrix, int _width, int _height)
    {
        for (int x = 0; x < _width; x++)
            for (int y = 0; y < _height; y++)
            {
                double num = random.Next(0, 100000) / 100000f * 100f;
                matrix[x, y] = num >= threshold;
            }
    }
}
