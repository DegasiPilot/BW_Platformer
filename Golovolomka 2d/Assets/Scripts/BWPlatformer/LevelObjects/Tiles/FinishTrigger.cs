using BWPlatformer;
using UnityEngine;

namespace BWPlatformer.LevelObjects.Tiles
{
    public class FinishTrigger : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(Tags.Player))
            {
                GameManager.Instance.Finish();
            }
        }
    }
}
