using DruidLib;
using System.Linq;
using UnityEngine;
using Unity.Mathematics;

public enum LandscapeNoise
{
    Perlin,
    Simplex
}

[CreateAssetMenu(fileName = "", menuName = "GeoGen/Landscape")]
public class Landscape : Geo
{
    public TileData tile;
    public LandscapeNoise landscapeNoise;
    public int minpower;
    public int maxpower;
    [Range(0f, 1f)]
    public float minHeightPercent;
    [Range(0f, 1f)]
    public float maxHeightPercent;
    
    public override void Generate(Map map, Dict<string> Params)
    {
        int seed = (int)Params.GetData("Seed") * 100;
        float[] heights = GetHeights(seed, map.width);

        TileData[,] layer = new TileData[map.width, map.height];

        int minHeight = (int)(map.height * minHeightPercent);
        int midHeight = (int)(map.height * (maxHeightPercent - minHeightPercent));

        for (int x = 0; x < map.width; x++)
            for (int y = 0; y < minHeight + midHeight * heights[x]; y++)
                layer[x, y] = tile;

        map.AddLayer(layer);
    }

    private float[] GetHeights (int seed, int width)
    {
        float[] heights = new float[width];

        for (int x = 0; x < width; x++)
        {
            float[] nums = new float[maxpower - minpower];

            for (int i = minpower; i < maxpower; i++)
            {
                float mult = Mathf.Pow(2, i);
                float h = GetNoiseHeight((int)(x * mult), seed, landscapeNoise);
                nums[i - minpower] = h;
            }

            float height = nums.Sum() / nums.Length;
            heights[x] = height;
        }

        return heights;
    }

    private float GetNoiseHeight (int x, int seed, LandscapeNoise _noise)
    {
        float height = 0;
        float2 point = new float2(x / 10f + seed, 0.5f);

        if (landscapeNoise == LandscapeNoise.Perlin)
            height = Mathf.PerlinNoise(point.x, point.y);
        else if (landscapeNoise == LandscapeNoise.Simplex)
            height = noise.snoise(point);

        return height;
    }
}
