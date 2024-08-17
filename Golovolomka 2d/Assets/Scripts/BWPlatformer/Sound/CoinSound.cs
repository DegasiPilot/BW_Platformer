using BWPlatformer.LevelObjects;
using UnityEngine;

namespace BWPlatformer.Sound
{
	[RequireComponent(typeof(Coin))]
	public class CoinSound : MonoBehaviour
	{
		[SerializeField] private SoundContainer _pickSound;

		private void Awake()
		{
			GetComponent<Coin>().OnPicked += PlayCoinSound;
		}

		private void PlayCoinSound()
		{
			SoundManager.Instance.PlayOneShot(_pickSound);
		}
	}
}
