using UnityEngine;

namespace BWPlatformer.LevelObjects
{
	public abstract class BrokeUnderPlayer : MonoBehaviour
	{
		[SerializeField] protected float _timeToBroke;

		protected YieldInstruction _waitInstruction;

		protected virtual void Awake()
		{
			_waitInstruction = new WaitForSeconds(_timeToBroke);
		}

		protected virtual void OnCollisionEnter2D(Collision2D collision)
		{
			Broke(collision);
		}

		protected abstract void Broke(Collision2D collision);
	}
}