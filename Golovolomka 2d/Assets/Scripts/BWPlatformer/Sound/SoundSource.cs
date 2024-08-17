using UnityEngine;

namespace BWPlatformer.Sound
{
	[RequireComponent(typeof(AudioSource))]
	public class SoundSource : MonoBehaviour
	{
		public AudioSource AudioSource { get; private set; }

		protected virtual void Awake()
		{
			AudioSource = GetComponent<AudioSource>();
		}

		public void SetActiveSound(SoundContainer soundContainer, float pitch = 1f)
		{
			if (AudioSource.clip != soundContainer.Clip)
			{
				AudioSource.clip = soundContainer.Clip;
				AudioSource.volume = soundContainer.Volume;
				AudioSource.pitch = pitch;
				AudioSource.loop = soundContainer.Loop;
				AudioSource.Play();
			}
			else if (!AudioSource.isPlaying || soundContainer.ForceReplay)
			{
				AudioSource.Play();
			}
		}
	}
}