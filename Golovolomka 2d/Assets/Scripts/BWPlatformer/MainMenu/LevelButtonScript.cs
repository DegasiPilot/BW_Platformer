using UnityEngine;
using UnityEngine.UI;

namespace BWPlatformer.MainMenu
{
	public class LevelButtonScript : MonoBehaviour
	{
		[SerializeField] private Text _levelNumberText;
		[SerializeField] private Button _button;

		public void Init(int levelNumber, bool isActive, System.Action<int> onClick)
		{
			_levelNumberText.text = levelNumber.ToString();
			_button.onClick.AddListener(() => onClick(levelNumber));
			_button.interactable = isActive;
		}
	}
}
