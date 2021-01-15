using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/RootsGG")]
public class RootsGG : Geo
{
    Generation gen;
    int Width;
    int Height;
    int seed;
    System.Random random;

    public TileData tile;
    public int CellCount;
    public float Thickness;
    public float VectorDist;
    public float MaxAngle;
    public float RootAngle;
    public float BranchAngle;
    public float Move;


    public override void Generate(Generation generation, params object[] _params)
    {
        //init
        gen = generation;
        Width = (int)_params[0];
        Height = (int)_params[1];
        seed = (int)_params[2];
        random = new System.Random((int)_params[2]);

        //gen
        int leftBound = (int)VectorDist;
        int rightBound = (int)(Width - 1 - VectorDist);

        Vector2Int point = new Vector2Int(Algorithms.Rand(leftBound, rightBound, seed), Height - 1);

        GenRoot(point, RootAngle, CellCount);
    }

    private void FillVector (List<Vector2Int> points, float _thickness)
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

            int minX = Mathf.Clamp((int)(points[i].x - _thickness + 0.5f), 0, Width);
            int maxX = Mathf.Clamp((int)(points[i].x + _thickness + 0.5f), 0, Width);
            int minY = Mathf.Clamp((int)(points[i].y - _thickness + 0.5f), 0, Height);
            int maxY = Mathf.Clamp((int)(points[i].y + _thickness + 0.5f), 0, Height);
            //and I now
            //it is not pretty
            
            for (int x = minX; x < maxX; x++)
                for (int y = minY; y < maxY; y++)
                {
                    float dist = Vector2Int.Distance(new Vector2Int(x, y), points[i]);
                    if (dist <= _thickness)
                        gen.tileMatrix[x, y].SetTileData(tile);
                }
        }
    }

    private void GenRoot (Vector2Int point, float angleBase, int generation)
    {
        //cos recursion needs to be stopped
        if (generation <= 0)
            return;

        //1000 / 1000, cos it must be random float multiplier
        float addAngle = MaxAngle * Algorithms.Rand(-1000, 1000, seed) / 1000;
        float curAngle = angleBase + addAngle;
        float mult = generation * 1f / CellCount;

        float dist = VectorDist * mult;
        float thickness = Thickness * mult;


        //this root cell
        List<Vector2Int> points = new List<Vector2Int>();
        GenPointArr(points, point, curAngle * Mathf.Deg2Rad, dist);
        FillVector(points, thickness);


        //next root
        GenRoot(points.Last(), curAngle, generation - 1);


        //branch root
        float randomCoin = Algorithms.Rand(0, 2, seed);
        float branchAngle = (randomCoin == 0 ? 1 : -1) * BranchAngle;

        GenRoot(points[points.Count - 1], curAngle + branchAngle, generation - 2);
    }

    private void GenPointArr (List<Vector2Int> arr, Vector2Int from, float angle, float dist)
    {
        for (float idist = 0; idist < dist; idist += Move)
        {
            float x = from.x;
            float y = from.y;
            x += idist * Mathf.Cos(angle);
            y += idist * Mathf.Sin(angle);
            Vector2Int point = new Vector2Int((int)x, (int)y);
            arr.Add(point);
        }
    }
}
