using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGeoGen : ScriptableObject
{
    public abstract TileData[,] GenGeo(Generation generation, int seed);
}
