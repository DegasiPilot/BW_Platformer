using System.Collections;
using UnityEngine;

namespace BWPlatformer.LevelObjects
{
	public class BrokableObject : BrokeUnderPlayer
	{
		private bool _isBroking;

		protected override void Broke(Collision2D collision)
		{
			if (!_isBroking)
			{
				_isBroking = true;
				StartCoroutine(BrokeRoutine());
			}
		}

		private IEnumerator BrokeRoutine()
		{
			yield return _waitInstruction;
			Destroy(gameObject);
			yield break;
		}
	}
}
