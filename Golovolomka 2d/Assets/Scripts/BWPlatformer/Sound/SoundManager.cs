using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace BWPlatformer.Sound
{
	public class SoundManager : SoundSource
	{
		public static SoundManager Instance { get; private set; }

		[SerializeField] private SoundSource _soundSourcePrefab;

		private ObjectPool<SoundSource> _soundSourcesPool;

		protected override void Awake()
		{
			if(Instance != null)
			{
				Destroy(gameObject);
				return;
			}
			Instance = this;
			DontDestroyOnLoad(gameObject);
			base.Awake();
			_soundSourcesPool = new(CreateSoundSource, defaultCapacity: 1, maxSize: 8);
		}

		private SoundSource CreateSoundSource()
		{
			return Instantiate(_soundSourcePrefab);
		}

		public void SetActiveMusic(SoundContainer soundContainer, float pitch = 1)
		{
			SetActiveSound(soundContainer, pitch);
		}

		public void PlayOneShot(SoundContainer soundContainer)
		{
			SoundSource soundSource = _soundSourcesPool.Get();
			soundSource.SetActiveSound(soundContainer);
		}

		private IEnumerator ReleaseRoutine(SoundSource soundSource)
		{
			yield return new WaitWhile(() => soundSource.AudioSource.isPlaying);
			_soundSourcesPool.Release(soundSource);
		}
	}
}
