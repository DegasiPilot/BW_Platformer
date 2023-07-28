using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSerialization : MonoBehaviour
{ 
    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath
          + "/MySaveData.dat", FileMode.OpenOrCreate);
        SaveData data = new SaveData();
        data.IsCoinsCollect = GameData.CollectedCoins;
        data.IsLevelsComplete = GameData.CompletedLevels;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

     public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            GameData.CollectedCoins = data.IsCoinsCollect;
            GameData.CompletedLevels = data.IsLevelsComplete;
            Debug.Log("Game data loaded!");
        }
        else
            Debug.Log("There is no save data!");
    }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/MySaveData.dat");
            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }


}

[Serializable]
class SaveData
{
    public bool[] IsCoinsCollect;
    public bool[] IsLevelsComplete;
}