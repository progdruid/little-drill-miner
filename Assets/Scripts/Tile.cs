using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector]
    public Vector2Int point;

    public TileData tileData;
    [SerializeField]
    private Generation gen;

    public TileData GetTileData ()
    {
        return tileData;
    }

    public void SetTileData (TileData _tileData)
    {
        if (_tileData == null)
            return;

        tileData = _tileData;
        Sprite sprite = tileData.sprite;
        UpdateSprite(sprite);
    }

    private void UpdateSprite (Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
