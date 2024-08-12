using UnityEngine;

namespace BWPlatformer.UI
{
	public class CanvasManager : MonoBehaviour
	{
		[SerializeField] private GameObject _finishPanel;
		[SerializeField] private GameObject _pausePanel;
		[SerializeField] private GameObject _mobileInputUI;

		public bool IsPausePaneleActive => _pausePanel.activeInHierarchy;

		private void Awake()
		{
			_mobileInputUI.SetActive(Application.isMobilePlatform);
		}

		public void SetActivePausePanel(bool active)
		{
			_pausePanel.SetActive(active);
		}

		public void OnFinish()
		{
			_finishPanel.SetActive(true);
		}
	}
}
