using UnityEngine;
using UnityEngine.UI;
using BWPlatformer.Enums;

namespace BWPlatformer.UI
{
	[RequireComponent(typeof(Text))]
	public class LocalizedText : MonoBehaviour
	{
		[SerializeField] private string _russianText;
		[SerializeField] private string _englishText;

		private Text _displayedText;

		private void Awake()
		{
			_displayedText = GetComponent<Text>();
			UpdateLocalizedText();
		}

		public void UpdateLocalizedText()
		{
			if (_displayedText != null)
			{
				_displayedText.text = GetLocalizedText(GameDataManager.UserLanguage);
			}
		}

		private string GetLocalizedText(Language language)
		{
			string localizedText = null;
			switch (language)
			{
				case Language.Russian:
					localizedText = _russianText;
					break;
				case Language.English:
					localizedText = _englishText;
					break;
			}
			if (string.IsNullOrEmpty(localizedText))
			{
				localizedText = "Error: no localized text!";
			}
			return localizedText;
		}
	}
}
