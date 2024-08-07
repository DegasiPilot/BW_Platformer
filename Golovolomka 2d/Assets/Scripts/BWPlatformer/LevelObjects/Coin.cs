using UnityEngine;

namespace BWPlatformer.LevelObjects
{
    public class Coin : MonoBehaviour
    {
        public bool IsPiked;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Player))
            {
                IsPiked = true;
                gameObject.SetActive(false);
            }
        }
    }
}
