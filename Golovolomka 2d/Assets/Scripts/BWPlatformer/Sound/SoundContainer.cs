using System;
using UnityEngine;

namespace BWPlatformer.Sound
{
	[Serializable]
	public class SoundContainer
	{
		public AudioClip Clip;
		[Range(0,1)]
		public float Volume;
		public bool ForceReplay;
		public bool Loop;
	}
}
