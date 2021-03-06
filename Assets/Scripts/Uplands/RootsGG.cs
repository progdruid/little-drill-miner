using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DruidLib;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/RootsGG")]
public class RootsGG : Geo
{
    int seed, x;

    public TileData tile;
    [Space]
    [Tooltip(tooltip: "Number of generations")]
    public int CellCount;
    public float Thickness;
    [Tooltip(tooltip: "Width of the first generation cell")]
    public float VectorDist;
    [Tooltip(tooltip: "Detail (difference between two circle points)")]
    public float deltaDist;
    [Space]
    public float RootAngle;
    [Tooltip(tooltip: "Max difference between angles of two cells")]
    public float MaxAngle;
    [Tooltip(tooltip: "Max difference between")]
    public float BranchAngle;

    private Map map;
    private TileData[,] layer;

    public override void Generate(Map _map, Dict<string> Params)
    {
        //init
        layer = new TileData[_map.width, _map.height];
        map = _map;
        seed = (int)Params.GetData("Seed");
        x = (int)Params.GetData("X");

        //gen
        Vector2Int point = new Vector2Int(x, map.height - 1);

        GenRoot(point, RootAngle, CellCount);

        _map.AddLayer(layer);
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

            int minX = Mathf.Clamp((int)(points[i].x - _thickness + 0.5f), 0, map.width);
            int maxX = Mathf.Clamp((int)(points[i].x + _thickness + 0.5f), 0, map.width);
            int minY = Mathf.Clamp((int)(points[i].y - _thickness + 0.5f), 0, map.height);
            int maxY = Mathf.Clamp((int)(points[i].y + _thickness + 0.5f), 0, map.height);
            //and I now
            //it is not pretty
            
            for (int x = minX; x < maxX; x++)
                for (int y = minY; y < maxY; y++)
                {
                    float dist = Vector2Int.Distance(new Vector2Int(x, y), points[i]);
                    if (dist <= _thickness)
                        layer[x, y] = tile;
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
        for (float idist = 0; idist < dist; idist += deltaDist)
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
