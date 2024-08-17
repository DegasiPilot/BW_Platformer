using UnityEngine;
using UnityEngine.SceneManagement;

namespace BWPlatformer
{
    public class LoadManager : MonoBehaviour
    {
        private void Awake()
        {
			GameDataManager.LoadGame();
        }

		private void Start()
		{
			SceneManager.LoadScene(1);
		}
	}
}
