using BWPlatformer.Interfaces;
using UnityEngine;

namespace BWPlatformer.LevelObjects
{
    [RequireComponent(typeof(Collider2D))]
    public class PortalScript : MonoBehaviour
    {
		[SerializeField] private Transform ExitPoint;
		[SerializeField] private PortalScript SecondPortal;

		private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ITeleportable teleportable))
            {
                teleportable.TeleportTo(SecondPortal.ExitPoint.position);
            }
        }
	}
}