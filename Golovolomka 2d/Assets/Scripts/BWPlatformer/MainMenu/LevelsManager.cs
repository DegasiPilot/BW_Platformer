using UnityEngine;
using System;

namespace BWPlatformer.MainMenu
{
	public class LevelsManager : MonoBehaviour
	{
		[SerializeField] private LevelButtonScript buttonPrefab;

		public void Init(int levelsCount, Action<int> OnLevelClick)
		{
			var levels = GameDataManager.CurrentData.CompletedLevels;
			int savedLevelsCount = levels.Count;
			Instantiate(buttonPrefab, transform).Init(1, true, OnLevelClick);
			for (int i = 1; i < levelsCount; i++)
			{
				Instantiate(buttonPrefab, transform).Init(i+1, 
					i - 1 < savedLevelsCount,
					OnLevelClick);
			}
		}
	}
}
