using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/SumGeo")]
public class Sum : Geo
{
    [SerializeField] Geo[] geos;

    public override void Generate(Map map, dynamic param)
    {
        for (int i = 0; i < geos.Length; i++)
        {
            (int seed, int width, int height) Config = param;
            Config.seed *= (i + 1);
            geos[i].Generate(map, Config);
        }
    }
}
