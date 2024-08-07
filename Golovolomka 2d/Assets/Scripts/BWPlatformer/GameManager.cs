using BWPlatformer.LevelObjects.Tiles;
using BWPlatformer.DataSaveSystem;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BWPlatformer.LevelObjects;
using UnityEngine.Tilemaps;

namespace BWPlatformer
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private int _levelNumber;
        [SerializeField] private Image _background;
        [SerializeField] private KeyCode _keyBackChange;
        [SerializeField] private GameObject _finishPanel;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private KeyCode _pauseKey;
        [SerializeField] private HideOnBackground[] _HideOnBackground;

        [SerializeField] private List<Text> _tips;
        [SerializeField] private Coin[] _coins;

        private bool _isLevelComleted;

		private void Awake()
		{
			Time.timeScale = 1;
			Instance = this;
			foreach (var tilePalette in _HideOnBackground)
			{
				tilePalette.ChangeState(_background.color);
			}
            _isLevelComleted = GameDataManager.CurrentData.CompletedLevels.Count >= _levelNumber;
            if (_isLevelComleted)
            {
                var levelInfo = GameDataManager.CurrentData.CompletedLevels[_levelNumber - 1];
				for (int i = 0; i < _coins.Length; i++)
				{
                    if (levelInfo.IsEarnedCoins[i]) _coins[i].gameObject.SetActive(false);
				}
			}
		}

		private void Update()
		{
			BackgroundUpdater();
			PauseChecker();
		}

		private void PauseChecker()
        {
            if (Input.GetKeyDown(_pauseKey) && Time.timeScale > 0)
            {
				_pausePanel.SetActive(true);
				Pause();
            }
        }

        private void BackgroundUpdater()
        {
            if (Input.GetKeyDown(_keyBackChange) && Time.timeScale > 0)
            {
                ChangeBackgroung();
            }
        }

        public void ChangeBackgroung()
        {
            if (_background.color == Color.white)
            {
                _background.color = Color.black;
            }
            else
            {
                _background.color = Color.white;
            }

            foreach(var tilePalette in _HideOnBackground)
            {
                tilePalette.ChangeState(_background.color);
			}

            ChangeColorForTips(_background.color);
        }

        private void ChangeColorForTips(Color backgroundColor)
        {
            foreach (Text tip in _tips)
            {
                tip.color = backgroundColor == Color.black ? Color.white : Color.black;
            }
        }

        public void DestroyTip(Text tip)
        {
            Destroy(tip.gameObject);
            _tips.Remove(tip);
        }

        public void NextLevel()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
			if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
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
            _finishPanel.SetActive(true);
            Time.timeScale = 0;
            if (_isLevelComleted)
            {
                var coins = GameDataManager.CurrentData.CompletedLevels[_levelNumber - 1].IsEarnedCoins;
                for(int i = 0; i < coins.Length; i++)
                {
                    coins[i] = coins[i] || _coins[i].IsPiked;
                }
			}
            else
            {
                var level = new LevelSaveData((from coin in _coins select coin.IsPiked).ToArray());
                GameDataManager.CurrentData.CompletedLevels.Add(level);
			}
			GameDataManager.SaveGame();
        }

        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void ContinueLevel()
        {
            Time.timeScale = 1;
            _pausePanel.SetActive(false);
        }
    }
}