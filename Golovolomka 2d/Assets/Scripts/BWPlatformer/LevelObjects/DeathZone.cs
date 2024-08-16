using BWPlatformer.Interfaces;
using UnityEngine;

namespace BWPlatformer.LevelObjects
{
    [RequireComponent(typeof(Collider2D))]
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.GetDamage();
            }
            else
            {
                Destroy(other);
            }
        }
    }
}
