using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private (int seed, int width, int height, int x, int y) Config;
    private Generation gen;

    private List<Vector2Int> rooms;
    private List<(Vector2Int, Vector2Int)> corridors;

    public override void Generate(Generation generation, dynamic Params)
    {
        Config = Params;
        gen = generation;

        rooms = new List<Vector2Int>();
        corridors = new List<(Vector2Int, Vector2Int)>();

        Vector2Int room = new Vector2Int(Config.x, Config.y);
        rooms.Add(room);

        GenDungeonGraph(room);


        for (int i = 0; i < roomCount; i++)
        {
            if (rooms[i].x < 0 || rooms[i].x >= Config.width || rooms[i].y < 0 || rooms[i].y >= Config.height)
                continue;
            DrawRoom(rooms[i]);
        }

        for (int i = 0; i < corridors.Count; i++)
            DrawCorridor(corridors[i]);

        CutCore();
    }

    private void GenDungeonGraph (Vector2Int firstRoom)
    {
        Vector2Int last = firstRoom;
        for (int i = 0; i < roomCount - 1; i++) //(roomCount - 1), cos we already have the first room
        {
            int rand = Algorithms.Rand(1, 5, Config.seed);
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

        for (int x = 1; x < Config.width - 1; x++)
            for (int y = 1; y < Config.height - 1; y++)
            {
                int dissonants = 0;
                
                for (int _x = x - 1; _x <= x + 1; _x++)
                    for (int _y = y - 1; _y <= y + 1; _y++)
                    {
                        if (_x == x && _y == y)
                            continue;
                
                        if (gen.tileMatrix[_x, _y].GetTileData() != DungeonTile)
                            dissonants++;
                    }
                
                if (dissonants != 0)
                    continue;

                needCut.Add((x, y));
            }

        for (int i = 0; i < needCut.Count; i++)
            gen.tileMatrix[needCut[i].x, needCut[i].y].SetTileData(VoidTile);
    }


    /*
    private void AddOtherCorridors ()
    {
        for (int i = 0; i < roomCount; i++)
        {
            for (int j = 0; j < roomCount; j++)
            {
                if (i == j)
                    continue;
                if (Vector2Int.Distance(rooms[i], rooms[j]) <= distance)
                {
                    Vector2Int firstCenter = new Vector2Int(rooms[i].x + roomWidth / 2, rooms[i].y + roomHeight / 2);
                    Vector2Int secondCenter = new Vector2Int(rooms[j].x + roomWidth / 2, rooms[j].y + roomHeight / 2);
                    (Vector2Int, Vector2Int) corridor = (firstCenter, secondCenter);
                    corridors.Add(corridor);
                }
            }
        }
    }*/

    private void DrawRoom (Vector2Int point)
    {
        //Creating empty rect of dungeon tile
        int maxX = Mathf.Clamp(point.x + roomWidth, 0, Config.width);
        int maxY = Mathf.Clamp(point.y + roomHeight, 0, Config.height);

        for (int x = point.x; x < maxX; x++)
            for (int y = point.y; y < maxY; y++)
            {
                gen.tileMatrix[x, y].SetTileData(DungeonTile);
            }

    }

    private void DrawCorridor ((Vector2Int, Vector2Int) corridor)
    {
        List<Vector2Int> choppedVector = Algorithms.ChopVector(corridor, deltaDist);

        for (int i = 0; i < choppedVector.Count; i++)
        {
            Vector2Int point = choppedVector[i];

            int minX = Mathf.Clamp(point.x - (int)corridorRadius, 0, Config.width);
            int minY = Mathf.Clamp(point.y - (int)corridorRadius, 0, Config.height);
            
            int maxX = Mathf.Clamp(point.x + (int)corridorRadius, 0, Config.width);
            int maxY = Mathf.Clamp(point.y + (int)corridorRadius, 0, Config.height);

            for (int x = minX; x < maxX; x++)
                for (int y = minY; y < maxY; y++)
                {
                    float circleDist = Vector2Int.Distance(point, new Vector2Int(x, y));
                    if (circleDist <= corridorRadius)
                        gen.tileMatrix[x, y].SetTileData(DungeonTile);
                }
        }

    }
}
