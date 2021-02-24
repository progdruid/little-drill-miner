using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Struct : MonoBehaviour
{
    public StructData structData { get; private set; }
    public GameObject go { get; private set; }

    public Struct(StructData data, GameObject _go)
    {
        structData = data;
        go = _go;
    }
    
    
}
