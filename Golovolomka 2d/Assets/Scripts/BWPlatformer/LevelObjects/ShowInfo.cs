using BWPlatformer;
using UnityEngine;

namespace BWPlatformer.LevelObjects.Tiles
{
    public class ShowInfo : MonoBehaviour
    {
        public GameObject info;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(Tags.Player))
            {
                info.SetActive(true);
                GameManager.Instance.Pause();
            }
        }
    }
}
