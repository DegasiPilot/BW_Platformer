using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Color MyColor;
    public static List<TileScript> AllTiles;

    private void Awake()
    {
        MyColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    public void ChangeState(Color BackColor)
    {
        if (MyColor == BackColor)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public static void ChangeStateForAll(Color BackColor)
    {
        for(int i=0; i<AllTiles.Count; i++)
        {
            try
            {
                AllTiles[i].ChangeState(BackColor);
            }
            catch
            {
                AllTiles.RemoveAt(i);
            }
        }
    }
}



