using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    //inspector only
    [SerializeField] private LayerData layer;
    [SerializeField] private GameObject Parent;
    [SerializeField] private GameObject EmptyGameObject;
    [SerializeField] private Transform minTransform;
    [SerializeField] private Transform maxTransform;
    [SerializeField] private int seed; //temp

    //non-inspector public fields (used in other classes)
    public Tile[,] tileMatrix;
    public TPoint minPoint;
    public TPoint maxPoint;

    //initializing nesesary components
    private void Init () 
    {
        minPoint = TPoint.Parse(minTransform.position);
        maxPoint = TPoint.Parse(maxTransform.position);
        tileMatrix = new Tile[maxPoint.x, maxPoint.y];
    }

    //temp
    private void Start()
    {
        Generate(seed);
    }

    //map generation
    public void Generate (int _seed)
    {
        seed = _seed;
        Init();

        CreateMapCore();
        GenerateGeos();
    }

    //creation core of the map: tile matrix and bg
    private void CreateMapCore () 
    {
        for (int x = minPoint.x; x < maxPoint.x; x++)
            for (int y = minPoint.y; y < maxPoint.y; y++) 
            {
                InstantiateCell(x, y);
                GenBack(layer.BackSprite, x, y);
            }
    }

    //instantiate a cell of tileMatrix
    private void InstantiateCell (int x, int y) 
    {
        GameObject go = Instantiate(EmptyGameObject, new Vector2(x, y), Quaternion.identity);
        Tile temp = (Tile)go.AddComponent(typeof(Tile));
        go.transform.SetParent(Parent.transform);
        tileMatrix[x, y] = temp;
    }

    //bg tile generation
    void GenBack (Sprite backSprite, int x, int y)
    {
        GameObject back = Instantiate(EmptyGameObject, new Vector3(x, y, 1), Quaternion.identity);
        back.GetComponent<SpriteRenderer>().sprite = backSprite;
        back.transform.SetParent(Parent.transform);
    }

    //generation of geos
    private void GenerateGeos ()
    {
        int _seed = seed;
        foreach (IGeoGen geo in layer.geos)
        {
            TileData[,] _tileMatrix = new TileData[maxPoint.x, maxPoint.y];
            _tileMatrix = geo.GenGeo(this, _seed);
            PlaceGeo(_tileMatrix); //place geo
            _seed++;
        }
    }

    //placing geo
    private void PlaceGeo (TileData[,] _tileMatrix) 
    {
        for (int x = minPoint.x; x < maxPoint.x; x++)
            for (int y = minPoint.y; y < maxPoint.y; y++)
            {
                TileData tile = _tileMatrix[x, y];
                tileMatrix[x, y].SetTileData(tile);
            }
    }
}
