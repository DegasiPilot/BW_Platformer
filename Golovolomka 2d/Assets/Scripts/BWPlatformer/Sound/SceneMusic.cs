using UnityEngine;

namespace BWPlatformer.Sound
{
	public class SceneMusic : MonoBehaviour
	{
		[SerializeField] private SoundContainer _sceneMusic;

		private void Awake()
		{
			SoundManager.Instance.SetActiveMusic(_sceneMusic);
		}
	}
}
