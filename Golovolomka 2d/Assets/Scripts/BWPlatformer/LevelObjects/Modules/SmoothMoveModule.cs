using System.Collections;
using UnityEngine;

namespace BWPlatformer.LevelObjects.Modules
{
	public class SmoothMoveModule : MonoBehaviour
	{
		public bool IsMoving => moveCoroutine != null;

		public GameObject GameObject => gameObject;

		public Vector3 Position => GameObject.transform.position;

		private Vector2 currentPos;
		private Coroutine moveCoroutine;

		public Coroutine MoveToPosition(Vector2 finalPos, float speed)
		{
			if (IsMoving) StopCoroutine(moveCoroutine);
			moveCoroutine = StartCoroutine(MoveToPositionRoutine(finalPos, speed));
			return moveCoroutine;
		}

		public IEnumerator MoveToPositionRoutine(Vector2 finalPos, float speed)
		{
			currentPos = gameObject.transform.position;
			do
			{
				currentPos = Vector2.MoveTowards(currentPos, finalPos, speed * Time.deltaTime);
				gameObject.transform.position = currentPos;
				yield return new WaitForEndOfFrame();
			} while (currentPos != finalPos);
		}

		public void StopMove()
		{
			if (IsMoving) StopCoroutine(moveCoroutine);
		}
	}
}