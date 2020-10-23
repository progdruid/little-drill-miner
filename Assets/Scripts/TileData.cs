using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "TileData")]
public class TileData : ScriptableObject
{
    public string tilename;

    public TileData[] dissonantTiles;

    [Space]

    public Sprite sprite;

}