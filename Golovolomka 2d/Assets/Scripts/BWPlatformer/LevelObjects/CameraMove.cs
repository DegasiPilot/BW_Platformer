using UnityEngine;

namespace BWPlatformer.Levels.LevelObjects
{

    public class CameraMove : MonoBehaviour
    {
		[SerializeField] private GameObject _player;

        void Update()
        {
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -10);
        }
    }
}