using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DruidLib;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/CountGeo")]
public class CountGeo : Geo
{
    [SerializeField] int count;
    [SerializeField] Geo geo;

    public override void Generate(Map map, Dict<string> Params)
    {
        for (int i = 0; i < count; i++)
        {
            geo.Generate(map, Params);
        }
    }
}
