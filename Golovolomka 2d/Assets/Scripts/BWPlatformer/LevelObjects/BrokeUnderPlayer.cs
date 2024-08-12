using System.Collections;
using UnityEngine;

namespace BWPlatformer.LevelObjects
{
	public class BrokeUnderPlayer : MonoBehaviour
	{
		[SerializeField] protected float _timeToBroke = 1f;

		private bool IsBroking;
		protected YieldInstruction _waitInstruction;

		protected virtual void Awake()
		{
			_waitInstruction = new WaitForSeconds(_timeToBroke);
		}

		protected virtual void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.collider.CompareTag(Tags.Player))
			{
				Broke(collision);
			}
		}

		protected virtual void Broke(Collision2D collision)
		{
			if (!IsBroking)
			{
				IsBroking = true;
				StartCoroutine(BrokeRoutine());
			}

			IEnumerator BrokeRoutine()
			{
				yield return _waitInstruction;
				Destroy(gameObject);
				yield break;
			}
		}
	}
}