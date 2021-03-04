using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DruidLib;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/CaveGG")]
public class CaveGeo : Geo
{
    //public
    public TileData tile;
    [Range(0, 100)]
    public double threshold;
    public int automataIters;
    public int rule;


    public override void Generate(Map map, Dict<string> Params)
    {
        int seed = (int)Params.GetData("Seed");

        TileData[,] layer = new TileData[map.width, map.height];

        //gen
        bool[,] automata = new bool[map.width, map.height];
        CreateRandomPoints(automata, map.width, map.height, seed);

        bool[,] turnTemp = new bool[map.width, map.height];
        for (int i = 0; i < automataIters; i++)
        {
            for (int x = 0; x < map.width; x++)
                for (int y = 0; y < map.height; y++)
                {
                    int count = 0;
                    for (int _x = x - 1; _x <= x + 1; _x++)
                        for (int _y = y - 1; _y <= y + 1; _y++)
                        {
                            if (_x == x && _y == y)
                                continue;

                            int modX = Algorithms.Mod(_x, map.width);
                            int modY = Algorithms.Mod(_y, map.height);

                            if (automata[modX, modY])
                                count++;
                        }

                    turnTemp[x, y] = Condition(count, automata[x, y]);
                }

            automata = turnTemp;
        }

        for (int x = 0; x < map.width; x++)
            for (int y = 0; y < map.height; y++)
            {
                if (automata[x, y])
                    layer[x, y] = tile;
            }

        map.AddLayer(layer);
    }

    private bool Condition (int count, bool self)
    {
        return count > rule;
    }

    private void CreateRandomPoints (bool[,] matrix, int _width, int _height, int seed)
    {
        System.Random random = new System.Random(seed);
        
        for (int x = 0; x < _width; x++)
            for (int y = 0; y < _height; y++)
            {
                double num = Algorithms.Rand(0, 100000, seed) / 100000f * 100f;
                matrix[x, y] = num >= threshold;
            }
    }
}
