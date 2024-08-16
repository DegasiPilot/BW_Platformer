using BWPlatformer.UI;
using UnityEngine;

namespace BWPlatformer.LevelObjects
{
	[RequireComponent(typeof(Collider2D))]
	public class ShowDialogue : MonoBehaviour
	{
		[SerializeField] private GameObject _dialoguePanel;
		[SerializeField] private LocalizedText _defaultAnswer;
		[SerializeField] private LocalizedText _notFitstAttemp;
		[SerializeField] private LocalizedText _allCoins;
		[SerializeField] private LocalizedText _notFitstAttempAllCoins;
		[SerializeField] private LocalizedText _afterColorsBackToWorld;
		private LocalizedText _activeAnswer;
		private bool _isComlete;
		private bool _isAllCoinsPicked;
		private bool _isAllColorsBackToWorld;

		public void OnNotFirstAttempt()
		{
			_isComlete = true;
		}

		public void OnAllMoneysPicked()
		{
			_isAllCoinsPicked = true;
		}

		public void OnAllColorBackToWorld()
		{
			_isAllColorsBackToWorld = true;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			_dialoguePanel.SetActive(true);

			if (_activeAnswer != null)
			{
				_activeAnswer.gameObject.SetActive(false);
			}
			_activeAnswer = GetAnswer();
			_activeAnswer.gameObject.SetActive(true);

			GameManager.Instance.Pause();
		}

		private LocalizedText GetAnswer()
		{
			if (_isAllColorsBackToWorld)
			{
				return _afterColorsBackToWorld;
			}
			else if (_isComlete && _isComlete)
			{
				return _notFitstAttempAllCoins;
			}
			else if (_isComlete)
			{
				return _notFitstAttemp;
			}
			else if (_isAllCoinsPicked) 
			{
				return _allCoins;
			}
			else
			{
				return _defaultAnswer;
			}
		}
	}
}