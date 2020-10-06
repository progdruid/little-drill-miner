using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/RootsGG")]
public class RootsGG : IGeoGen
{
    int Seed;
    Generation gen;
    TileData[,] tileMatrix;
    System.Random random;

    public TileData tile;
    public int generationsCount;
    public float Thickness;
    public float VectorDist;
    public float MaxAngle;
    public float RootAngle;
    public float BranchAngle;

    public override TileData[,] GenGeo(Generation generation, int seed)
    {
        //init
        Seed        = seed;
        gen         = generation;

        tileMatrix  = new TileData[gen.maxPoint.x, gen.maxPoint.y];

        //gen
        random = new System.Random(Seed);
        TPoint point = new TPoint(random.Next((int)(VectorDist), (int)(gen.maxPoint.x - 1 - VectorDist)), gen.maxPoint.y - 1);
        GenRoot(point, RootAngle, generationsCount);

        return tileMatrix;
    }

    private void FillVector (List<TPoint> points, float _thickness)
    {
        //cos very small numbers are always a bad idea when you work with PIXELS, Carl
        //they will start to create needless pixels (tiles)
        if (_thickness < 0.5f)
            return;

        for (int i = 0; i < points.Count; i++)
        {
            //Clamp is for protection
            //0.5f is for better look, cos floats in generation are always better than integers
            //floats are cool
            //if you add 0.5f, it will be better than 1

            int minX = Mathf.Clamp((int)(points[i].x - _thickness + 0.5f), 0, gen.maxPoint.x);
            int maxX = Mathf.Clamp((int)(points[i].x + _thickness + 0.5f), 0, gen.maxPoint.x);
            int minY = Mathf.Clamp((int)(points[i].y - _thickness + 0.5f), 0, gen.maxPoint.y);
            int maxY = Mathf.Clamp((int)(points[i].y + _thickness + 0.5f), 0, gen.maxPoint.y);
            //and I now
            //it is not pretty
            
            for (int x = minX; x < maxX; x++)
                for (int y = minY; y < maxY; y++)
                {
                    float dist = TPoint.Dist(new TPoint(x, y), points[i]);
                    if (dist <= _thickness)
                        tileMatrix[x, y] = tile;
                }
        }
    }

    private void GenRoot (TPoint point, float angleBase, int generation)
    {
        //1000 / 1000, cos it must be random float multiplier
        float addAngle = MaxAngle * random.Next(-1000, 1000) / 1000;
        float curAngle = angleBase + addAngle;
        float mult = generation * 1f / generationsCount;
        float dist = VectorDist * mult;
        float thickness = Thickness * mult;

        //cos recursion, it needs to be stopped
        if (generation <= 0)
            return;

        List<TPoint> points = new List<TPoint>();
        GenPointArr(points, point, curAngle * Mathf.Deg2Rad, dist);
        FillVector(points, thickness);

        GenRoot(points[points.Count - 1], curAngle, generation - 1);

        //it is this case when
        //if it works, dont change it
        float randomCoin = random.Next(0, 2);
        float branchAngle = BranchAngle;
        if (randomCoin == 1)
            branchAngle = -BranchAngle;

        GenRoot(points[points.Count - 1], curAngle + branchAngle, generation - 2);
    }

    private void GenPointArr (List<TPoint> arr, TPoint from, float angle, float dist)
    {
        for (float idist = 0; idist < dist; idist++)
        {
            float x = from.x;
            float y = from.y;
            x += idist * math.cos(angle);
            y += idist * math.sin(angle);
            TPoint point = new TPoint((int)x, (int)y);
            arr.Add(point);
        }
    }
}
