using BWPlatformer.Player;
using UnityEngine;

namespace BWPlatformer.Sound
{
	[RequireComponent(typeof(PlayerController))]
	public class PlayerSound : SoundSource
	{
		[SerializeField] SoundContainer _jumpSound;
		[SerializeField] SoundContainer _walkSound;

		private PlayerController _player;

		protected override void Awake()
		{
			base.Awake();
			_player = GetComponent<PlayerController>();
			_player.OnJump += PlayerJump;
			_player.OnWalk += PlayerMove;
		}

		private void PlayerJump()
		{
			SetActiveSound(_jumpSound);
		}

		private void PlayerMove(float normalizedSpeed)
		{
			if (normalizedSpeed != 0)
			{
				SetActiveSound(_walkSound, normalizedSpeed);
			}
		}
	}
}
