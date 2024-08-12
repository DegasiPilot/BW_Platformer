using System;
using UnityEngine;

namespace BWPlatformer.Input
{
	public abstract class BaseInputReader : MonoBehaviour
	{
		public abstract event Action<Vector2> OnMoveInput;
		public abstract event Action OnJumpInput;
		public abstract event Action OnChangeBackgroundInput;
		public abstract event Action OnPauseInput;
	}
}