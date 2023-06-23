using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image Background;
    public KeyCode KeyBackChange;
    public List<Text> Tips;

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

        ChangeColorForTips(Background.color);
    }

    private void ChangeColorForTips(Color backgroundColor)
    {
        foreach(Text tip in Tips)
        {
            tip.color = backgroundColor == Color.black ? Color.white : Color.black;
        }
    }

    public void RestartLevel()
    {
        TileScript.AllTiles = new List<TileScript>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
