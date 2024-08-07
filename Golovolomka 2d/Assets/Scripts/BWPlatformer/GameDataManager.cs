using BWPlatformer.DataSaveSystem;
using System.IO;
using UnityEngine;
using BWPlatformer.Enums;

namespace BWPlatformer
{
	public static class GameDataManager
	{
		private const string saveFileName = "SaveData.dat";
		private static readonly string dataPath = Path.Combine(Application.persistentDataPath, saveFileName);

		public static GameSaveData CurrentData { get; private set; } = new();
		public static Language UserLanguage { get; set; } = Language.English;

		public static void SaveGame()
		{
			try
			{
				using (var stream = File.Open(dataPath, FileMode.OpenOrCreate))
				{
					BinarySerializator.Serialize(CurrentData, stream);
				}
			}
			catch
			{
				Debug.LogError("Error on Saving!");
			}
		}

		public static void LoadGame()
		{
			if (File.Exists(dataPath))
			{
				FileStream file = File.Open(dataPath, FileMode.Open);
				try
				{
					CurrentData = BinarySerializator.Deserialize<GameSaveData>(file);
					Debug.Log("Data loaded!");
				}
				finally
				{
					file.Close();
				}
			}
			else
			{
				Debug.Log("There is no save data!");
			}
			try
			{
				UserLanguage = (Language)PlayerPrefs.GetInt("Language");
			}
			catch { }
		}


		public static void ResetData()
		{
			if (File.Exists(dataPath))
			{
				File.Delete(dataPath);
				Debug.Log("Data reset complete!");
			}
			else
			{
				Debug.LogError("No save data to delete.");
			}
		}
	}
}