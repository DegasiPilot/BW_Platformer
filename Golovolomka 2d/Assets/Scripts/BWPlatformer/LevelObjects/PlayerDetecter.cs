using UnityEngine;
using UnityEngine.Events;

namespace BWPlatformer.LevelObjects
{
	[RequireComponent(typeof(Collider2D))]
	public class PlayerDetecter : MonoBehaviour
	{
		[SerializeField] private UnityEvent<Collider2D> OnTriggerOrCollision;
		[SerializeField] private bool _onlyOnсe;

		private bool _hadhappened;

		private void OnTriggerEnter2D(Collider2D collider)
		{
			CollisionOrTrigger(collider);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			CollisionOrTrigger(collision.otherCollider);
		}

		private void CollisionOrTrigger(Collider2D collider)
		{
			if (!_onlyOnсe || !_hadhappened)
			{
				_hadhappened = true;
				OnTriggerOrCollision.Invoke(collider);
			}
		}

		public void AddListener(UnityAction<Collider2D> call)
		{
			OnTriggerOrCollision.AddListener(call);
		}

		public void RemoveListener(UnityAction<Collider2D> call)
		{
			OnTriggerOrCollision.RemoveListener(call);
		}
	}
}
