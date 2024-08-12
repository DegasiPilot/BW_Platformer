using BWPlatformer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BWPlatformer.Player
{
    public class GroundChecker : MonoBehaviour, IGroundChecker
    {
		public bool IsGrounded { get; private set; }

        public event System.Action OnLanded;
        public event System.Action OnJumped;

        private HashSet<Collider2D> _collisionsColiders = new(4);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(Tags.Player))
            {
				_collisionsColiders.Add(other);
                if(_collisionsColiders.Count == 1)
                {
                    OnLanded?.Invoke();
                    IsGrounded = true;
                }
			}
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(Tags.Player))
            {
				_collisionsColiders.Remove(other);
				if (_collisionsColiders.Count == 0)
				{
					OnJumped?.Invoke();
					IsGrounded = false;
				}
			}
        }

        public void OnBackgroundColorChanged()
        {
            UpdateCollisions();
        }

        private void UpdateCollisions()
        {
            _collisionsColiders.RemoveWhere(c => !c.gameObject.activeInHierarchy);
            bool LastGroundedState = IsGrounded;
			IsGrounded = _collisionsColiders.Count > 0;
            if(LastGroundedState && !IsGrounded)
            {
				OnJumped?.Invoke();
			}
        }
    }
}