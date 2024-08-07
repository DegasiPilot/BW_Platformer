using BWPlatformer;
using BWPlatformer.Enums;
using System.Collections;
using UnityEngine;

namespace BWPlatformer.LevelObjects
{
    public class PortalScript : MonoBehaviour
    {
		[SerializeField] private Transform ExitPoint;
		[SerializeField] private PortalScript SecondPortal;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Player))
            {
                other.transform.position = SecondPortal.ExitPoint.position;
            }
        }
	}
}