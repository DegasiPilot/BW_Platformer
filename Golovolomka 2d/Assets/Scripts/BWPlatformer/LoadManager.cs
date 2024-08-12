using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace BWPlatformer
{
    public class LoadManager : MonoBehaviour
    {
        public UnityEvent OnLoaded;

        private void Awake()
        {
			GameDataManager.LoadGame();
            OnLoaded.Invoke();
            SceneManager.LoadScene(1);
        }
    }
}
