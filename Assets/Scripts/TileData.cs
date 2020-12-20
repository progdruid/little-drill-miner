using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "TileData")]
public class TileData : ScriptableObject
{
    public string tilename;
    public TileData[] dissonantTiles;
    public Texture2D texture;

    private const int bor = 16;
    
    public Sprite[] tileset;
    // 0 - main sprite - 0000
    //
    // 1 - up - 0001
    // 2 - right - 0010
    // 3 - up-right - 0011
    // 4 - down - 0100
    // 
    // 5 - up-down - 0101
    // 6 - right-down - 0110
    // 7 - up-right-down - 0111
    // 8 - left - 1000
    // 
    // 9 - up-left - 1001
    // 10 - right-left - 1010
    //
    // 11 - up-right-left - 1011
    // 12 - down-left - 1100
    // 13 - up-down-left - 1101
    // 14 - right-down-left - 1110
    //
    // 15 - all - 1111
    
    /*
    public void OnValidate()
    {
        if (texture.width <= bor && texture.height <= bor)
        {
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            Sprite sprite = Sprite.Create(texture, rect, new Vector2(0, 0));
            for (int i = 0; i < tileset.Length; i++)
                tileset[i] = sprite;
            return;
        }

        tileset = new Sprite[16];
        tileset[0] = Parse(texture, 1 * bor, 1 * bor, bor, bor);
        tileset[1] = Parse(texture, 1 * bor, 2 * bor, bor, bor);
        tileset[2] = Parse(texture, 0 * bor, 1 * bor, bor, bor);
        tileset[3] = Parse(texture, 0 * bor, 2 * bor, bor, bor);
        tileset[4] = Parse(texture, 1 * bor, 0 * bor, bor, bor);
        tileset[5] = Parse(texture, 1 * bor, 3 * bor, bor, bor);
        tileset[6] = Parse(texture, 0 * bor, 0 * bor, bor, bor);
        tileset[7] = Parse(texture, 0 * bor, 3 * bor, bor, bor);
        tileset[8] = Parse(texture, 2 * bor, 1 * bor, bor, bor);
        tileset[9] = Parse(texture, 2 * bor, 2 * bor, bor, bor);
        tileset[10] = Parse(texture, 3 * bor, 1 * bor, bor, bor);
        tileset[11] = Parse(texture, 3 * bor, 2 * bor, bor, bor);
        tileset[12] = Parse(texture, 2 * bor, 0 * bor, bor, bor);
        tileset[13] = Parse(texture, 2 * bor, 3 * bor, bor, bor);
        tileset[14] = Parse(texture, 3 * bor, 0 * bor, bor, bor);
        tileset[15] = Parse(texture, 3 * bor, 3 * bor, bor, bor);
    }
    */
    private Sprite Parse (Texture2D texture, int x, int y, int width, int height)
    {
        Color[] colorArr = texture.GetPixels(x, y, width, height);
        Texture2D newTexture = new Texture2D(width, height);
        newTexture.SetPixels(colorArr);
        Rect rect = new Rect(0, 0, width, height);
        Vector2 vector = new Vector2(0, 0);
        Sprite sprite = Sprite.Create(newTexture, rect, vector);
        return sprite;
    }
}