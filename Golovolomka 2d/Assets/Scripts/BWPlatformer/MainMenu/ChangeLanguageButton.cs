using BWPlatformer.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace BWPlatformer.MainMenu
{
	[RequireComponent(typeof(Button))]
	public class ChangeLanguageButton : MonoBehaviour
	{
		[SerializeField] MenuManager _menuManager;
		[SerializeField] Language _language;

		private void Awake()
		{
			Button button = GetComponent<Button>();
			button.onClick.AddListener(() => _menuManager.SetLanguage(_language));
		}
	}
}
