using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DruidLib;

public abstract class Geo : ScriptableObject
{
    public abstract void Generate(Map map, Dict<string> Params);

}
