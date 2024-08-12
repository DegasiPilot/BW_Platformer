using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BWPlatformer.Input
{
	public class InputReader : BaseInputReader, InputActions.IPlayerActions
	{
		public override event Action<Vector2> OnMoveInput;
		public override event Action OnJumpInput;
		public override event Action OnChangeBackgroundInput;
		public override event Action OnPauseInput;

		private InputActions _inputActions;

		private void Awake()
		{
			_inputActions = new();
			_inputActions.Player.SetCallbacks(this);
			_inputActions.Player.Enable();
		}

		public void OnChangeBackground(InputAction.CallbackContext context)
		{
			if (context.phase == InputActionPhase.Performed)
			{
				OnChangeBackgroundInput?.Invoke();
			}
		}

		public void OnJump(InputAction.CallbackContext context)
		{
			if (context.phase == InputActionPhase.Performed)
			{
				OnJumpInput?.Invoke();
			}
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			if (context.phase != InputActionPhase.Started)
			{
				OnMoveInput?.Invoke(context.ReadValue<Vector2>());
			}
		}

		public void OnPause(InputAction.CallbackContext context)
		{
			if (context.phase == InputActionPhase.Performed)
			{
				OnPauseInput?.Invoke();
			}
		}
	}
}