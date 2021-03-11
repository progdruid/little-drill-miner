using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Layer")]
public class LayerData : ScriptableObject
{
    public string Name;

    public Sprite BackSprite;

    public int BGLayersCount;
    public Sprite[] BGLayers;
    [Range(-1f, 1f)]
    public float[] BGDistances;

    public float XRepeating;
    public float YRepeating;

    public Geo EntryGeo;

    private void OnValidate()
    {
        Sprite[] tempLayers = BGLayers;
        float[] tempDists = BGDistances;

        BGLayers = new Sprite[BGLayersCount];
        BGDistances = new float[BGLayersCount];

        for (int i = 0; i < tempLayers.Length && i < BGLayersCount; i++)
        {
            BGLayers[i] = tempLayers[i];
            BGDistances[i] = tempDists[i];
        }
    }
}
