using UnityEngine;
using UnityEngine.SceneManagement;

namespace BWPlatformer
{
    public class LoadManager : MonoBehaviour
    {
        private void Start()
        {
			GameDataManager.LoadGame();
            SceneManager.LoadScene(1);
        }
    }
}
