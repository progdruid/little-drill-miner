using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TPoint
{
    public int x;
    public int y;

    public TPoint() { }

    public TPoint(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public bool IsSame(TPoint point)
    {
        if (x != point.x)
            return false;
        if (y != point.y)
            return false;

        return true;
    }

    public static float Dist (TPoint a, TPoint b)
    {
        int width = math.abs(a.x - b.x);
        int height = math.abs(a.y - b.y);
        float res = math.sqrt(width * width + height * height);
        return res;
    }

    public static TPoint Parse (Vector2 vector)
    {
        TPoint point = new TPoint();
        point.x = Mathf.RoundToInt(vector.x);
        point.y = Mathf.RoundToInt(vector.y);
        return point;
    }

    public static TPoint operator + (TPoint a, TPoint b)
    {
        TPoint point = new TPoint(a.x + b.x, a.y + b.y);
        return point;
    }

    public static TPoint operator -(TPoint a, TPoint b)
    {
        TPoint point = new TPoint(a.x - b.x, a.y - b.y);
        return point;
    }
}
