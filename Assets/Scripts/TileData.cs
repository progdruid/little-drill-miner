using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "TileData")]
public class TileData : ScriptableObject
{
    public string tilename;
    public Sprite sprite;

    public double cost;
    public double mass;
    public double difficulty;
}