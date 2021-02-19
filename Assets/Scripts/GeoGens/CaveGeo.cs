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


    public override void Generate(Map map, dynamic param)
    {
        (int seed, int width, int height) Config = param;

        TileData[,] layer = new TileData[map.width, map.height];

        //gen
        bool[,] automata = new bool[Config.width, Config.height];
        CreateRandomPoints(automata, Config.width, Config.height, Config.seed);

        for (int i = 0; i < automataIters; i++)
            Algorithms.CellAutomataTurn(ref automata, Config.width, Config.height, ConditionFunc);

        for (int x = 0; x < Config.width; x++)
            for (int y = 0; y < Config.height; y++)
            {
                if (automata[x, y])
                    layer[x, y] = tile;
            }

        map.AddLayer(layer);
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
