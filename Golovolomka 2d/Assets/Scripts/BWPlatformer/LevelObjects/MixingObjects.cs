using BWPlatformer.LevelObjects.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BWPlatformer.LevelObjects
{
    public class MixingObjects : MonoBehaviour
    {
        [SerializeField] private SmoothMoveModule[] _objectsToMix;
		[SerializeField] private float _mixTime;
        [SerializeField] private float _speed;
        [SerializeField] private float _leftX, _rightX, _upY, _downY;
        [SerializeField] private UnityEvent OnStartMix;
        [SerializeField] private UnityEvent OnEndMix;

        private List<Vector2> _startPositions;

        private void Start()
        {
			_startPositions = new List<Vector2>(_objectsToMix.Length);
            for (int i = 0; i < _objectsToMix.Length; i++)
            {
                _startPositions.Add((Vector2)_objectsToMix[i].Position);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            StartCoroutine(StartMix());
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        public IEnumerator StartMix()
        {
            OnStartMix.Invoke();
            Coroutine[] movePortalCoroutines = new Coroutine[_objectsToMix.Length];
            for (int i = 0; i < _objectsToMix.Length; i++)
            {
				movePortalCoroutines[i] = StartCoroutine(MovingObject(_objectsToMix[i]));
            }
            yield return new WaitForSeconds(_mixTime);
            for (int i = 0; i < _objectsToMix.Length; i++)
            {
				StopCoroutine(movePortalCoroutines[i]);
                _objectsToMix[i].StopMove();
				int randomIndex = Random.Range(0, _startPositions.Count);
                _objectsToMix[i].MoveToPosition(_startPositions[randomIndex], _speed);
                _startPositions.RemoveAt(randomIndex);
            }
            OnEndMix.Invoke();
            Destroy(gameObject);
        }

        private IEnumerator MovingObject(SmoothMoveModule moveableObject)
        {
            while (true)
            {
				yield return moveableObject.MoveToPositionRoutine(GetRandomPos(), _speed);
            }
        }

        public Vector2 GetRandomPos()
        {
            Debug.Log("random pos");
            return new Vector2(
                Random.Range(_leftX, _rightX),
                Random.Range(_downY, _upY));
        }
    }
}