using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/CaveGG")]
public class CaveGeo : Geo
{
    //public
    public TileData tile;
    [Range(0, 100)]
    public double threshold;
    public int automataIters;
    public int rule;

    public override void Generate(Generation generation, int width, int height, int seed)
    {
        //gen
        bool[,] automata = new bool[width, height];
        CreateRandomPoints(automata, width, height, seed);

        for (int i = 0; i < automataIters; i++)
            Algorithms.CellAutomataTurn(ref automata, width, height, ConditionFunc);

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                if (automata[x, y])
                    generation.tileMatrix[x, y].SetTileData(tile);
            }

    }

    private bool ConditionFunc (int count)
    {
        return count >= rule;
    }

    private void CreateRandomPoints (bool[,] matrix, int _width, int _height, int seed)
    {
        System.Random random = new System.Random(seed);
        
        for (int x = 0; x < _width; x++)
            for (int y = 0; y < _height; y++)
            {
                double num = random.Next(0, 100000) / 100000f * 100f;
                matrix[x, y] = num >= threshold;
            }
    }
}
