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
    public GameObject FinishPanel;
    public GameObject PausePanel;
    public KeyCode PauseKey;

    private void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(BackgroundUpdater());
        StartCoroutine(PauseChecker());
    }

    private IEnumerator PauseChecker()
    {
        while (true)
        {
            if (Input.GetKeyDown(PauseKey))
                Pause();
            yield return null;
        }
    }

    public IEnumerator BackgroundUpdater()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyBackChange))
                ChangeBackgroung();
            yield return null;
        }
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
        TileScript.AllTiles = new List<TileScript>();
        SceneManager.LoadScene(0);
    }

    public void Finish()
    {
        FinishPanel.SetActive(true);
        Time.timeScale = 0;
        StopAllCoroutines(); // BackgroundUpdater() PauseChecker()
        TileScript.AllTiles = new List<TileScript>();
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        StopAllCoroutines(); // BackgroundUpdater() PauseChecker()
    }

    public void ContinueLevel()
    {
        Time.timeScale = 1;
        StartCoroutine(BackgroundUpdater());
        StartCoroutine(PauseChecker());
        PausePanel.SetActive(false);
    }
}
