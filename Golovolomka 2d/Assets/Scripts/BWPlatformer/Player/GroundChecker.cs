using BWPlatformer.Interfaces;
using UnityEngine;

namespace BWPlatformer.Player
{
    public class GroundChecker : MonoBehaviour, IGroundChecker
    {
        [SerializeField] private Vector2 _colliderSize;
        [SerializeField] private LayerMask _layerMask;

        public bool IsGrounded => CheckGrounded();

        private Collider2D[] _collisionsColiders = new Collider2D[1];
        private ContactFilter2D _contactFilter = new();

		private void Awake()
		{
			_contactFilter.SetNormalAngle(225, 315);
			_contactFilter.SetLayerMask(_layerMask);
		}

		private bool CheckGrounded()
		{
            int collisonsCount = Physics2D.OverlapBox(transform.position, _colliderSize, 0, _contactFilter, _collisionsColiders);
            return collisonsCount > 0;
		}
	}
}