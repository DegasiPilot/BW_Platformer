using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject levelsPanel;
    public GameObject[] coins;

    public void Exit()
    {
        Application.Quit();
    }

    public void StartLevel(int levelNumber)
    {
        SceneManager.LoadScene("Level " + levelNumber);
    }

    private void Start()
    {
        try
        {
            bool check = GameData.CollectedCoins[0];
        }
        catch
        {
            GameData.CollectedCoins = new bool[coins.Length];
        }

        for (int i = 0; i < coins.Length; i++)
        {
            if (GameData.CollectedCoins[i])
                coins[i].GetComponent<Image>().color = Color.white;
        }
    }
}
