using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithms
{
    private static int randCount;

    public static int Rand(int min, int max, int seed)
    {
        randCount++;
        seed *= randCount;
        System.Random random = new System.Random(seed);
        int res = random.Next(min, max);
        return res;
    }

    public static float Rand(float min, float max, int seed)
    {
        randCount++;
        seed *= randCount;
        System.Random random = new System.Random(seed);
        return min + (max - min) * random.Next(0, 100000) / 100000f;
    }

    public static List<Vector2Int> ChopVector ((Vector2Int, Vector2Int) vector, float deltaDist)
    {
        List<Vector2Int> choppedVector = new List<Vector2Int>();

        float dist = Vector2Int.Distance(vector.Item1, vector.Item2);
        int xDiff = vector.Item2.x - vector.Item1.x;
        int yDiff = vector.Item2.y - vector.Item1.y;
        float addX = deltaDist * xDiff / dist;
        float addY = deltaDist * yDiff / dist;

        choppedVector.Add(vector.Item1);
        for (int i = 0; i < dist / deltaDist; i++)
        {
            Vector2Int newVector = new Vector2Int(vector.Item1.x + (int)(addX * i), vector.Item1.y + (int)(addY * i));
            choppedVector.Add(newVector);
        }
        choppedVector.Add(vector.Item2);

        return choppedVector;
    }

    public static int Mod (int num, int divider)
    {
        return (num % divider + divider) % divider;
    }

    public static float Perlin (float x, float y, float seed, float xMult, float yMult)
    {
        float fX = x * 0.1f / xMult + seed;
        float fY = y * 0.1f / yMult + seed;
        float res = UnityEngine.Mathf.PerlinNoise(fX, fY);
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
