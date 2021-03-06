using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithms
{
    private static int randCount;

    public static int Rand(int min, int max, int seed)
    {
        randCount++;
        System.Random preRandom = new System.Random(randCount);
        seed += preRandom.Next(randCount, randCount * 1000);

        System.Random random = new System.Random(seed);
        int res = random.Next(min, max);
        return res;
    }

    public static int Mod (int num, int divider)
    {
        return (num % divider + divider) % divider;
    }

    public static float PerlinNoise (float x, float y, float seed)
    {
        float fX = seed * seed + x * 0.1f;
        float fY = seed * seed + y * 0.1f;
        float res = UnityEngine.Mathf.PerlinNoise(fX, fY);
        return res;
    }

    public static float Perlin (float x, float y, float seed, float xMult, float yMult)
    {
        float fX = x * 0.1f / xMult + seed;
        float fY = y * 0.1f / yMult + seed;
        float res = UnityEngine.Mathf.PerlinNoise(fX, fY);
        res *= res * 100;
        return res;
    }
}
