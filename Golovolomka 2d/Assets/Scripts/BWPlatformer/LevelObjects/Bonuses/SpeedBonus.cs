using UnityEngine;
using BWPlatformer.Player;

namespace BWPlatformer.LevelObjects.Bonuses
{
	public class SpeedBonus : TemporaryBonus
	{
		[SerializeField] private float _speedBonus;

		protected override void OnBonusPicked(PlayerController bonusPickuper)
		{
			base.OnBonusPicked(bonusPickuper);
			bonusPickuper.Speed += _speedBonus;
		}

		protected override void OnBonusEnd()
		{
			_bonusOwner.Speed -= _speedBonus;
			base.OnBonusEnd();
		}
	}
}