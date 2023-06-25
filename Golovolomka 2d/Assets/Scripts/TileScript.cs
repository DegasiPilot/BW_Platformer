using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Color MyColor;
    public static List<TileScript> AllTiles;

    private void Start()
    {
        MyColor = gameObject.GetComponent<SpriteRenderer>().color;
        try{
            AllTiles.Add(this);
        }
        catch
        {
            AllTiles = new List<TileScript>();
            AllTiles.Add(this);
        }
    }

    public static void ChangeStateForAll(Color BackColor)
    {
        foreach (TileScript tile in AllTiles)
            tile.ChangeState(BackColor);
    }

    private void ChangeState(Color BackColor)
    {
       if(MyColor == BackColor)
       {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
       }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

}



