using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    //inspector only
    [SerializeField] private LayerData layer;
    [SerializeField] private GameObject Parent;
    [SerializeField] private GameObject EmptyTile;
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;

    [SerializeField] private int Seed; //temp

    //non-inspector public fields (used in other classes)
    [HideInInspector] public Vector2Int minPoint;
    [HideInInspector] public Vector2Int maxPoint;


    //private fields
    public Tile[,] tileMatrix;

    //temp
    private void Start()
    {
        Generate(Seed);
    }

    //map generation
    public void Generate (int _seed)
    {
        //temporary nonsense
        Seed = _seed;

        Init();

        CreateMapCore();
        GenerateGeos(Seed);
    }

    //initializing nesesary components
    private void Init()
    {
        minPoint = Vector2Int.RoundToInt(minPos.position);
        maxPoint = Vector2Int.RoundToInt(maxPos.position);
        tileMatrix = new Tile[maxPoint.x, maxPoint.y];
    }

    //creation core of the map: tile matrix and bg
    private void CreateMapCore () 
    {
        for (int x = minPoint.x; x < maxPoint.x; x++)
            for (int y = minPoint.y; y < maxPoint.y; y++) 
            {
                Vector2Int point = new Vector2Int(x, y);
                
                CreateTile(point);
                CreateBack(layer.BackSprite, point);
            }
        for (int x = minPoint.x; x < maxPoint.x; x++)
            for (int y = minPoint.y; y < maxPoint.y; y++)
            {
                try { tileMatrix[x, y].upTile = tileMatrix[x, y + 1]; } catch { tileMatrix[x, y].upTile = new Tile(null); }
                try { tileMatrix[x, y].rightTile = tileMatrix[x + 1, y]; } catch { tileMatrix[x, y].rightTile = new Tile(null); }
                try { tileMatrix[x, y].downTile = tileMatrix[x, y - 1]; } catch { tileMatrix[x, y].downTile = new Tile(null); }
                try { tileMatrix[x, y].leftTile = tileMatrix[x - 1, y]; } catch { tileMatrix[x, y].leftTile = new Tile(null); }
            }
    }

    //instantiate a tile in tileMatrix
    private void CreateTile (Vector2Int point) 
    {
        GameObject go = Instantiate(EmptyTile, (Vector2)point, Quaternion.identity);
        go.transform.SetParent(Parent.transform);

        Tile tile = (Tile)go.AddComponent(typeof(Tile));
        //tile.point = point;
        
        tileMatrix[point.x, point.y] = tile;
    }

    //bg tile generation
    void CreateBack (Sprite backSprite, Vector2Int point)
    {
        GameObject back = Instantiate(EmptyTile, new Vector3(point.x, point.y, 1), Quaternion.identity);
        back.GetComponent<SpriteRenderer>().sprite = backSprite;
        
        back.transform.SetParent(Parent.transform);
    }

    //generation of geos
    private void GenerateGeos (int _seed)
    {
        foreach (Geo geo in layer.geos)
        {
            geo.Generate(this, ( seed: _seed, width: maxPoint.x, height: maxPoint.y ));
            _seed++;
        }
    }
}
