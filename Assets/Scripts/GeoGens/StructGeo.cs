using DruidLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/Struct")]
public class StructGeo : Geo
{
    public StructData[] Structs;

    public override void Generate(Map map, Dict<string> Params)
    {
        int seed = (int)Params.GetData("Seed");
        int x = (int)Params.GetData("X");
        int y = (int)Params.GetData("Y");

        StructData curStruct = Structs[Algorithms.Rand(0, Structs.Length, seed)];

        map.CreateStruct(x, y, curStruct);
    }
}
