using UnityEngine;
using UnityEngine.Events;

namespace BWPlatformer
{
	public class CheckAllMoneysCollected : MonoBehaviour
	{
		[SerializeField] private UnityEvent OnAllMoneysCollected;

		public void Awake()
		{
			foreach(var level in GameDataManager.CurrentData.CompletedLevels)
			{
				foreach(var coin in level.IsEarnedCoins)
				{
					if (coin == false) return;
				}
			}
			OnAllMoneysCollected.Invoke();
		}
	}
}
