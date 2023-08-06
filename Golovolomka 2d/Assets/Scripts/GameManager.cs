using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        TileScript.AllTiles = FindObjectsOfType<TileScript>().ToList();
        TileScript.ChangeStateForAll(Background.color);
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

    public void DestroyTip(int index)
    {
        Destroy(Tips[index].gameObject);
        Tips.RemoveAt(index);
    }
    public void NextLevel()
    {
        try
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        catch { }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Finish()
    {
        FinishPanel.SetActive(true);
        Time.timeScale = 0;
        StopAllCoroutines(); // BackgroundUpdater() PauseChecker()
        string SceneName = SceneManager.GetActiveScene().name;
        int levelNumber = Convert.ToInt32(SceneName.Split(' ')[1]);
        GameData.CompletedLevels[levelNumber - 1] = true;
        SaveSerialization saveSerialization = new SaveSerialization();
        saveSerialization.SaveGame();
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
