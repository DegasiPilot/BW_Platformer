using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Color MyColor;
    public static TileScript[] AllTiles;

    private void Start()
    {
        MyColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    public static void ChangeStateForAll(Color BackColor)
    {
        foreach (TileScript tile in AllTiles)
        {
            if (tile.MyColor == BackColor)
            {
                tile.gameObject.SetActive(false);
            }
            else
            {
                tile.gameObject.SetActive(true);
            }
        }
    }
}



