using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DruidLib;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/SumGeo")]
public class Sum : Geo
{
    [SerializeField] Geo[] geos;

    public override void Generate(Map map, Dict<string> Params)
    {
        for (int i = 0; i < geos.Length; i++)
        {
            geos[i].Generate(map, Params);
        }
    }
}
