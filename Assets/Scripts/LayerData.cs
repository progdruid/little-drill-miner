using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Layer")]
public class LayerData : ScriptableObject
{
    public string Name;

    public Sprite BackSprite;

    public List<Geo> geos;
}
