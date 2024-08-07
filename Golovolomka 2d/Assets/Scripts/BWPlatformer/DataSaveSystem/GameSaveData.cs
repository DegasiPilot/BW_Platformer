using System;
using System.Collections.Generic;

namespace BWPlatformer.DataSaveSystem
{
	[Serializable]
	public class GameSaveData
	{
		public readonly List<LevelSaveData> CompletedLevels = new();
	}
}
