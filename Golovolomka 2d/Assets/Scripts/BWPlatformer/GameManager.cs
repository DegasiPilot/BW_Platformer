using BWPlatformer.DataSaveSystem;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using BWPlatformer.LevelObjects;
using BWPlatformer.UI;
using UnityEngine.Events;
using BWPlatformer.Input;

namespace BWPlatformer
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

		[SerializeField] private UnityEvent<Color> _onBackgroundChanged;

        [SerializeField] private int _levelNumber;
        [SerializeField] private CanvasManager _canvasManager;
		[SerializeField] private BaseInputReader _inputReader;

        [SerializeField] private Coin[] _coins;

        private bool _isLevelComleted;
        private bool _backgroungChangeInput;
        private bool _pauseInput;

        private Camera _mainCamera;

		private void Awake()
		{
			Time.timeScale = 1;
			Instance = this;
            _mainCamera = Camera.main;
#if UNITY_EDITOR
            if (_mainCamera == null) throw new System.Exception("No camera on level!");
#endif
            _isLevelComleted = GameDataManager.CurrentData.CompletedLevels.Count >= _levelNumber;
            if (_isLevelComleted)
            {
                var levelInfo = GameDataManager.CurrentData.CompletedLevels[_levelNumber - 1];
				for (int i = 0; i < _coins.Length; i++)
				{
                    if (levelInfo.IsEarnedCoins[i]) _coins[i].gameObject.SetActive(false);
				}
			}

            _inputReader.OnChangeBackgroundInput += OnBackgroundChangeInput;
            _inputReader.OnPauseInput += OnPauseInput;
		}

		private void Start()
		{
			_onBackgroundChanged.Invoke(_mainCamera.backgroundColor);
		}

		private void Update()
		{
            if (_backgroungChangeInput)
            {
                ChangeBackgroung();
                _backgroungChangeInput = false;
            }
            if (_pauseInput)
            {
				if (Time.timeScale > 0)
				{
					_canvasManager.SetActivePausePanel(true);
					Pause();
				}
				else if(_canvasManager.IsPausePaneleActive)
				{
					ContinueLevel();
				}
                _pauseInput = false;
			}
		}

		private void OnPauseInput()
        {
            if (!_pauseInput)
            {
                _pauseInput = true;
			}
        }

        private void OnBackgroundChangeInput()
        {
			if (!_backgroungChangeInput && Time.timeScale > 0)
            {
                _backgroungChangeInput = true;
            }
        }

        private void ChangeBackgroung()
        {
            Color backgroundColor = _mainCamera.backgroundColor;
			if (backgroundColor == Color.white)
			{
				backgroundColor = Color.black;
			}
			else
			{
				backgroundColor = Color.white;
			}
            _mainCamera.backgroundColor = backgroundColor;

			_onBackgroundChanged.Invoke(backgroundColor);
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
            _canvasManager.OnFinish();
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
            _canvasManager.SetActivePausePanel(false);
        }
	}
}