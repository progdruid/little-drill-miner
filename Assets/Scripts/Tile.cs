using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private TileData tileData;

    public TileData GetTileData ()
    {
        return tileData;
    }

    public void SetTileData (TileData _tileData)
    {
        if (_tileData == null)
            return;

        tileData = _tileData;
        UpdateSprite(tileData.sprite);
    }

    private void UpdateSprite (Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
