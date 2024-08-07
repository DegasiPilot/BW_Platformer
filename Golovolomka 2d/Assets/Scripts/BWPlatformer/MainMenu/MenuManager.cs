using BWPlatformer.Enums;
using BWPlatformer.MainMenu;
using BWPlatformer.UI;
using System.Linq;

#if DEBUG
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BWPlatformer
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private EarnedLevelCoins[] _coins;
        [SerializeField] private int[] _sceneIndexes;
        [SerializeField] private LevelsManager _levelsManager;
        [SerializeField] private LocalizedText[] _localizedTexts;

		private void Awake()
        {
            _levelsManager.Init(_sceneIndexes.Count(), StartLevel);
			var savedLevels = GameDataManager.CurrentData.CompletedLevels;
            int levelsCount = savedLevels.Count;
			for (int i = 0; i < _coins.Length; i++)
            {
				if (_coins[i].LevelNumber <= levelsCount)
                {
                    var level = savedLevels[_coins[i].LevelNumber - 1];
                    if(level.IsEarnedCoins != null)
                    {
                        _coins[i].Init(level.IsEarnedCoins);
                    }
                }
			}
        }

        public void Exit()
        {
			GameDataManager.SaveGame();
            Application.Quit();
#if DEBUG
            EditorApplication.isPlaying = false;
#endif
        }

        public void StartLevel(int levelNumber)
        {
            SceneManager.LoadScene(_sceneIndexes[levelNumber-1]);
        }

        public void SetLanguage(Language language)
        {
            if (GameDataManager.UserLanguage != language)
            {
                GameDataManager.UserLanguage = language;
                foreach (var text in _localizedTexts)
                {
                    text.UpdateLocalizedText();
                }
                PlayerPrefs.SetInt("Language", (int)language);
            }
        }
    }
}