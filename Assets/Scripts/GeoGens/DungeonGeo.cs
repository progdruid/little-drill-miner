using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/DungeonGeo")]
public class DungeonGeo : Geo
{
    [SerializeField] TileData DungeonTile;
    [SerializeField] TileData VoidTile; //temp

    [SerializeField] int roomWidth, roomHeight, doorSize, thickness;
    //[SerializeField] float minMult, maxMult;

    private int worldWidth, worldHeight;
    private Generation gen;

    public override void Generate(Generation generation, dynamic Params)
    {
        (int seed, int width, int height, int x, int y) Config = Params;
        worldWidth = Config.width;
        worldHeight = Config.height;
        gen = generation;

        //float mult = Algorithms.Rand(minMult, maxMult, Config.seed);

        RoomState roomState = new RoomState();
        roomState.x = Config.x;
        roomState.y = Config.y;
        roomState.width = (int)(roomWidth);
        roomState.height = (int)(roomHeight);

        CreateRoom(roomState);
    }

    private void CreateRoom (RoomState roomState)
    {
        //Creating filled rect of dungeon tile
        int maxX = Mathf.Clamp(roomState.x + roomState.width, 0, worldWidth);
        int maxY = Mathf.Clamp(roomState.y + roomState.height, 0, worldHeight);
        for (int x = roomState.x; x < maxX; x++)
            for (int y = roomState.y; y < maxY; y++)
            {
                gen.tileMatrix[x, y].SetTileData(DungeonTile);
                if (x >= roomState.x + thickness && x < maxX - thickness && y >= roomState.y + thickness && y < maxY - thickness)
                    gen.tileMatrix[x, y].SetTileData(VoidTile);
            }

    }
}
