using UnityEngine;

namespace BWPlatformer.LevelObjects
{
    [RequireComponent(typeof(Collider2D))]
    public class Coin : MonoBehaviour
    {
        public bool IsPiked;

        private void OnTriggerEnter2D(Collider2D other)
        {
            IsPiked = true;
            gameObject.SetActive(false);
        }
    }
}
