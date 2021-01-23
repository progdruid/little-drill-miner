using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/SumGeo")]
public class Sum : Geo
{
    [SerializeField] Geo[] geos;

    public override void Generate(Generation generation, params object[] _params)
    {
        for (int i = 0; i < geos.Length; i++)
        {
            geos[i].Generate(generation, _params);
        }
    }
}
