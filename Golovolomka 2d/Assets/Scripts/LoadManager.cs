using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    private void Start()
    {
        SaveSerialization SaveLoadSystem = GetComponent<SaveSerialization>();
        Debug.Log(Application.persistentDataPath);
        SaveLoadSystem.LoadGame();
        SceneManager.LoadScene(1); 
    }
}
