using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Geo : ScriptableObject
{
    public abstract void Generate(Generation generation, params object[] _params);

}
