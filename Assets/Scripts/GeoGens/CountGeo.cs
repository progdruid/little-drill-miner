using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/CountGeo")]
public class CountGeo : Geo
{
    [SerializeField] int count;
    [SerializeField] Geo geo;

    public override void Generate(Generation generation, params object[] _params)
    {
        for (int i = 0; i < count; i++)
        {
            geo.Generate(generation, _params);
        }
    }
}
