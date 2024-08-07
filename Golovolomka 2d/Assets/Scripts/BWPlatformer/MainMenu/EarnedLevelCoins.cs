using UnityEngine;
using UnityEngine.UI;

namespace BWPlatformer.MainMenu
{
	public class EarnedLevelCoins : MonoBehaviour
	{
		public int LevelNumber;
		[SerializeField] private Image[] _coins;

		public void Init(bool[] isCoinsCollected)
		{
			for(int i = 0; i < isCoinsCollected.Length; i++)
			{
				_coins[i].color = Color.white;
			}
		}
	}
}