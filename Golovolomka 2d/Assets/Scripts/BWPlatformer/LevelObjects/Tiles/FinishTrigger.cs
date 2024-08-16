using UnityEngine;

namespace BWPlatformer.LevelObjects.Tiles
{
    [RequireComponent(typeof(Collider2D))]
    public class FinishTrigger : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameManager.Instance.Finish();
        }
    }
}
