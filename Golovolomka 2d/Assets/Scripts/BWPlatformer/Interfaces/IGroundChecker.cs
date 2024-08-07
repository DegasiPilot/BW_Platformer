using System;

namespace BWPlatformer.Interfaces
{
	public interface IGroundChecker
	{
		public bool IsGrounded { get; }
		public event Action OnLanded;
		public event Action OnJumped;
	}
}
