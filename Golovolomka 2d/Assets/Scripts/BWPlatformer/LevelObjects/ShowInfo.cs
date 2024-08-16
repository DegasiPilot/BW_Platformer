using UnityEngine;

namespace BWPlatformer.LevelObjects.Tiles
{
    public class ShowInfo : MonoBehaviour
    {
        public GameObject info;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            info.SetActive(true);
            GameManager.Instance.Pause();
        }
    }
}
