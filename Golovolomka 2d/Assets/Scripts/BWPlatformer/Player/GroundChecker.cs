using BWPlatformer.Interfaces;
using UnityEngine;

namespace BWPlatformer.Player
{
    public class GroundChecker : MonoBehaviour, IGroundChecker
    {
        public bool IsGrounded => _collisionsCount > 0;
        public event System.Action OnLanded;
        public event System.Action OnJumped;

        private int _collisionsCount;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(Tags.Player))
            {
                _collisionsCount++;
                if(_collisionsCount == 1)
                {
                    OnLanded?.Invoke();
                }
			}
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(Tags.Player))
            {
				_collisionsCount--;
				if (_collisionsCount == 0)
				{
					OnJumped?.Invoke();
				}
			}
        }
    }
}