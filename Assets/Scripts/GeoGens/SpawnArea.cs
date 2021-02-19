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

    public override void Generate(Map map, dynamic param)
    {
        (int seed, int width, int height) Config = param;

        int _x = (int)(Algorithms.Rand(start.x, end.x, Config.seed) / 100f * map.width);
        int _y = (int)(Algorithms.Rand(start.y, end.y, Config.seed) / 100f * map.height);

        geo.Generate(map, (seed: Config.seed, width: map.width, height: map.height, x: _x, y: _y));
    }


}
