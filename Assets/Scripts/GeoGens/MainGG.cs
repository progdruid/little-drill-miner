using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "GeoGen/MainGG")]
public class MainGG : IGeoGen
{
    private TileData[,] tileMatrix;
    private Generation _gen;

    public TileData MainTile;
    public int randomPointsCount;

    public override TileData[,] GenGeo(Generation generation, int seed)
    {
        _gen = generation;

        tileMatrix = new TileData[_gen.maxPoint.x, _gen.maxPoint.y];
        
        for (int x = 0; x < _gen.maxPoint.x; x++)
            for (int y = 0; y < _gen.maxPoint.y; y++)
            {
                tileMatrix[x, y] = MainTile;
            }

        return tileMatrix;
    }
}
