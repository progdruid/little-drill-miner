using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class Algorithms
{
    private static int randCount;

    public static int Rand(int min, int max, int seed)
    {
        randCount++;
        seed *= randCount;
        System.Random random = new System.Random(seed);
        return random.Next(min, max);
    }

    public static int Mod (int num, int divider)
    {
        return (num % divider + divider) % divider;
    }

    public static float Perlin (float x, float y, float seed, float xMult, float yMult)
    {
        float fX = x * 0.1f / xMult + seed;
        float fY = y * 0.1f / yMult + seed;
        float res = Mathf.PerlinNoise(fX, fY);
        res *= res * 100;
        return res;
    }

    public static void CellAutomataTurn(ref bool[,] automata, int width, int height, System.Func<int, bool> rule)
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                int count = GetNearsCount(automata, x, y, width, height);

                automata[x, y] = rule(count);
            }
    }

    private static int GetNearsCount (bool[,] automata, int pointX, int pointY, int width, int height)
    {
        int count = 0;
        for (int x = pointX - 1; x <= pointX + 1; x++)
            for (int y = pointY - 1; y <= pointY + 1; y++)
            {
                if (x == pointX && y == pointY)
                    continue;

                int modX = Mod(x, width);
                int modY = Mod(y, height);

                if (automata[modX, modY])
                    count++;
            }
        return count;
    }
}
