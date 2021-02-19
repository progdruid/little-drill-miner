using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DruidLib;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/DungeonGeo")]
public class DungeonGeo : Geo
{
    [SerializeField] TileData DungeonTile;
    [SerializeField] TileData VoidTile; //temp

    [SerializeField] int roomWidth, roomHeight, thickness;
    [SerializeField] int roomCount;
    [SerializeField] int distance;
    [SerializeField] float deltaDist;
    [SerializeField] float corridorRadius;

    private int seed;
    private Map Map;
    private TileData[,] layer;

    private List<Vector2Int> rooms;
    private List<(Vector2Int, Vector2Int)> corridors;

    public override void Generate(Map map, Dict<string> Params)
    {
        seed = (int)Params.GetData("Seed");
        Map = map;

        layer = new TileData[map.width, map.height];
        rooms = new List<Vector2Int>();
        corridors = new List<(Vector2Int, Vector2Int)>();

        Vector2Int room = new Vector2Int((int)Params.GetData("X"), (int)Params.GetData("Y"));
        rooms.Add(room);

        GenDungeonGraph(room);


        for (int i = 0; i < roomCount; i++)
        {
            if (rooms[i].x < 0 || rooms[i].x >= Map.width || rooms[i].y < 0 || rooms[i].y >= Map.height)
                continue;
            DrawRoom(rooms[i]);
        }

        for (int i = 0; i < corridors.Count; i++)
            DrawCorridor(corridors[i]);

        CutCore();

        map.AddLayer(layer);
    }

    private void GenDungeonGraph (Vector2Int firstRoom)
    {
        Vector2Int last = firstRoom;
        for (int i = 0; i < roomCount - 1; i++) //(roomCount - 1), cos we already have the first room
        {
            int rand = Algorithms.Rand(1, 5, seed);
            float angle = 2 * Mathf.PI / 4 * rand;

            //float angle = Algorithms.Rand(0f, 2 * Mathf.PI, Config.seed);

            int addX = (int)(Mathf.Cos(angle) * distance);
            int addY = (int)(Mathf.Sin(angle) * distance);

            Vector2Int next = new Vector2Int(last.x + addX, last.y + addY);

            Vector2Int lastCenter = new Vector2Int(last.x + roomWidth / 2, last.y + roomHeight / 2);
            Vector2Int nextCenter = new Vector2Int(next.x + roomWidth / 2, next.y + roomHeight / 2);
            (Vector2Int, Vector2Int) corridor = (lastCenter, nextCenter);

            rooms.Add(next);
            corridors.Add(corridor);

            last = next;
        }
    }

    private void CutCore ()
    {
        List<(int x, int y)> needCut = new List<(int x, int y)>();

        for (int x = 1; x < Map.width - 1; x++)
            for (int y = 1; y < Map.height - 1; y++)
            {
                int dissonants = 0;
                
                for (int _x = x - 1; _x <= x + 1; _x++)
                    for (int _y = y - 1; _y <= y + 1; _y++)
                    {
                        if (_x == x && _y == y)
                            continue;
                
                        if (Map.GetTileAt(_x, _y) != DungeonTile)
                            dissonants++;
                    }
                
                if (dissonants != 0)
                    continue;

                needCut.Add((x, y));
            }

        for (int i = 0; i < needCut.Count; i++)
            layer[needCut[i].x, needCut[i].y] = VoidTile;
    }

    private void DrawRoom (Vector2Int point)
    {
        //Creating empty rect of dungeon tile
        int maxX = Mathf.Clamp(point.x + roomWidth, 0, Map.width);
        int maxY = Mathf.Clamp(point.y + roomHeight, 0, Map.height);

        for (int x = point.x; x < maxX; x++)
            for (int y = point.y; y < maxY; y++)
            {
                layer[x, y] = DungeonTile;
            }

    }

    private void DrawCorridor ((Vector2Int, Vector2Int) corridor)
    {
        List<Vector2Int> choppedVector = Algorithms.ChopVector(corridor, deltaDist);

        for (int i = 0; i < choppedVector.Count; i++)
        {
            Vector2Int point = choppedVector[i];

            int minX = Mathf.Clamp(point.x - (int)corridorRadius, 0, Map.width);
            int minY = Mathf.Clamp(point.y - (int)corridorRadius, 0, Map.height);
            
            int maxX = Mathf.Clamp(point.x + (int)corridorRadius, 0, Map.width);
            int maxY = Mathf.Clamp(point.y + (int)corridorRadius, 0, Map.height);

            for (int x = minX; x < maxX; x++)
                for (int y = minY; y < maxY; y++)
                {
                    float circleDist = Vector2Int.Distance(point, new Vector2Int(x, y));
                    if (circleDist <= corridorRadius)
                        layer[x, y] = DungeonTile;
                }
        }

    }
}
