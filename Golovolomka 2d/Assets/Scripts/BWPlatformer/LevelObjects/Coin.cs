using System;
using UnityEngine;

namespace BWPlatformer.LevelObjects
{
    [RequireComponent(typeof(Collider2D))]
    public class Coin : MonoBehaviour
    {
        public bool IsPiked;
        public event Action OnPicked;

        private void OnTriggerEnter2D(Collider2D other)
        {
            IsPiked = true;
            gameObject.SetActive(false);
            OnPicked?.Invoke();
        }
    }
}
