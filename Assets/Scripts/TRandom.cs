using System;

public class TRandom
{
    public static int Next (int min, int max, int seed)
    {
        Random random = new Random(seed);
        return random.Next(min, max);
    }

    public static float Next (float min, float max, int seed)
    {
        Random random = new Random(seed);
        return min + (max - min) * random.Next(0, int.MaxValue) / int.MaxValue;
    }

    public static double Next(double min, double max, int seed)
    {
        Random random = new Random(seed);
        return min + (max - min) * random.Next(0, int.MaxValue) / int.MaxValue;
    }
}
