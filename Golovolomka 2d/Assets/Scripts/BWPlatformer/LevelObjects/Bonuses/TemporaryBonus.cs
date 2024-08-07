using BWPlatformer.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BWPlatformer.LevelObjects.Bonuses
{
	public abstract class TemporaryBonus : Bonus
	{
		/// <summary>
		/// In seconds
		/// </summary>
		[SerializeField] protected float _bonusTime;

		protected YieldInstruction _waitInstruction;
		protected PlayerController _bonusOwner;

		/// <summary>
		/// Initialize _waitInstruction
		/// </summary>
		protected override void Awake()
		{
			base.Awake();
			_waitInstruction = new WaitForSeconds(_bonusTime);
		}

		/// <summary>
		/// Invoke ActiveBonusesHandler and DelayedBonusEnd, add itself to ActiveBonuses, set _bonusOwner
		/// </summary>
		protected override void OnBonusPicked(PlayerController bonusPickuper)
		{
			base.OnBonusPicked(bonusPickuper);
			_bonusOwner = bonusPickuper;
			ActiveBonusesHandler(bonusPickuper.ActiveBonuses);
			bonusPickuper.ActiveBonuses.Add(this);
			StartCoroutine(DelayedBonusEnd());
		}

		/// <summary>
		/// Is invoked by OnBonusPicked. Remove bonuses of same type
		/// </summary>
		protected virtual void ActiveBonusesHandler(ICollection<TemporaryBonus> ActiveBonuses)
		{
			var bonusesOfSameType = ActiveBonuses.Where(b => b.BonusType == BonusType).ToList();
			foreach(var bonus in bonusesOfSameType)
			{
				if (bonus != null)
				{
					bonus.DeleteBonus();
				}
			}
		}

		/// <summary>
		/// Is invoked by OnBonusPicked as Coroutine
		/// </summary>
		protected virtual IEnumerator DelayedBonusEnd()
		{
			yield return _waitInstruction;
			OnBonusEnd();
		}

		/// <summary>
		/// Is invoked when bonus time ends. Destoy gameObject
		/// </summary>
		protected virtual void OnBonusEnd()
		{
			_bonusOwner.ActiveBonuses.Remove(this);
			Destroy(gameObject);
		}

		/// <summary>
		/// By default invoke OnBonusEnd
		/// </summary>
		public virtual void DeleteBonus()
		{
			OnBonusEnd();
		}

		protected override void AfterBonusApply() { }
	}
}