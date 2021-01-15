using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/RandomPosGen")]
public class SpawnArea : Geo
{
    [Tooltip("The count of spawning geos")]
    [SerializeField] int count;

    [Tooltip("The start point of spawn area in percents")]
    [SerializeField] Vector2Int start;
    [Tooltip("The end point of spawn area in percents")]
    [SerializeField] Vector2Int end;

    [SerializeField] Geo geo;

    private int width, height, seed;

    public override void Generate(Generation generation, params object[] _params)
    {
        width = (int)_params[0];
        height = (int)_params[1];
        seed = (int)_params[2];

        for (int i = 0; i < count; i++)
        {
            
            int x = Algorithms.Rand(start.x, end.x, seed) / 100 * width;
            int y = Algorithms.Rand(start.y, end.y, seed) / 100 * height;

            geo.Generate(generation, x, y);
        }


    }


}
