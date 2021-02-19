using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DruidLib;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/SpawnArea")]
public class SpawnArea : Geo
{
    [Tooltip("The start point of spawn area in percents")]
    [SerializeField] Vector2Int start;
    [Tooltip("The end point of spawn area in percents")]
    [SerializeField] Vector2Int end;

    [SerializeField] Geo geo;

    public override void Generate(Map map, Dict<string> Params)
    {
        int seed = (int)Params.GetData("Seed");

        int _x = (int)(Algorithms.Rand(start.x, end.x, seed) / 100f * map.width);
        int _y = (int)(Algorithms.Rand(start.y, end.y, seed) / 100f * map.height);

        Params.Add("X", _x);
        Params.Add("Y", _y);

        geo.Generate(map, Params);

        Params.Remove("X"); Params.Remove("Y");
    }


}
