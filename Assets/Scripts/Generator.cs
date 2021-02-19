using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private LayerData Layer;
    [SerializeField] private GameObject Parent;
    [SerializeField] private GameObject EmptyTile;

    [SerializeField] private int Width, Height;

    [SerializeField] private int Seed;

    private void Start()
    {
        Gen(Seed);
    }

    private void Gen (int seed)
    {
        Map map = new Map(CreateMap(), Width, Height);
        CreateBack(Layer.BackSprite);
        GenGeos(Seed);
    }

    private Tile[,] CreateMap ()
    {
        Tile[,] _tilemap = new Tile[Width, Height];

        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
            {
                GameObject go = Instantiate(EmptyTile, new Vector3(x, y, 0), Quaternion.identity);
                go.transform.SetParent(Parent.transform);

                Tile tile = (Tile)go.AddComponent(typeof(Tile));
                _tilemap[x, y] = tile;
            }

        ConnectTiles(_tilemap);

        return _tilemap;
    }

    private void ConnectTiles (Tile[,] _tilemap)
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
            {
                if (y + 1 < Height)
                    _tilemap[x, y].upTile = _tilemap[x, y + 1];
                if (x + 1 < Width)
                    _tilemap[x, y].rightTile = _tilemap[x + 1, y];
                if (y - 1 >= 0)
                    _tilemap[x, y].downTile = _tilemap[x, y - 1];
                if (x - 1 >= 0)
                    _tilemap[x, y].leftTile = _tilemap[x - 1, y];
            }
    }

    private void CreateBack (Sprite backSprite)
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
            {
                GameObject back = Instantiate(EmptyTile, new Vector3(x, y, 1), Quaternion.identity);
                back.GetComponent<SpriteRenderer>().sprite = backSprite;

                back.transform.SetParent(Parent.transform);
            }
    }

    private void GenGeos (int seed)
    {
        foreach (Geo geo in Layer.geos)
        {
            geo.Generate(this, (seed: seed, width: Width, height: Height));
            seed++;
        }
    } 
}
