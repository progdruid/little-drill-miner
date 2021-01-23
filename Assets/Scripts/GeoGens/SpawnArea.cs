using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/SpawnArea")]
public class SpawnArea : Geo
{
    [Tooltip("The start point of spawn area in percents")]
    [SerializeField] Vector2Int start;
    [Tooltip("The end point of spawn area in percents")]
    [SerializeField] Vector2Int end;

    [SerializeField] Geo geo;

    private int width, height, seed;

    public override void Generate(Generation generation, params object[] _params)
    {
        seed = (int)_params[0];
        width = (int)_params[1];
        height = (int)_params[2];

        int x = (int)(Algorithms.Rand(start.x, end.x, seed) / 100f * width);
        int y = (int)(Algorithms.Rand(start.y, end.y, seed) / 100f * height);

        geo.Generate(generation, seed, width, height, x, y);
    }


}
