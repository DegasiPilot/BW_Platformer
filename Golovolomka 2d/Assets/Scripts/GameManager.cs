using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image Background;
    public KeyCode KeyBackChange;

    public void Update()
    {
        if (Input.GetKeyDown(KeyBackChange))
            ChangeBackgroung();
    }

    public void ChangeBackgroung()
    {
        if(Background.color == Color.white)
            Background.color = Color.black;
        else
            Background.color = Color.white;
        TileScript.ChangeStateForAll(Background.color);
    }
}
