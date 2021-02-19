using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/CountGeo")]
public class CountGeo : Geo
{
    [SerializeField] int count;
    [SerializeField] Geo geo;

    public override void Generate(Map map, dynamic param)
    {
        for (int i = 0; i < count; i++)
        {
            (int seed, int width, int height) Config = param;
            Config.seed *= (i + 1);
            geo.Generate(map, Config);
        }
    }
}
