using System;

namespace BWPlatformer.DataSaveSystem
{
	[Serializable]
	public class LevelSaveData
	{
		public bool[] IsEarnedCoins;

		public LevelSaveData() { }

		public LevelSaveData(bool[] isEarnedCoins)
		{
			IsEarnedCoins = isEarnedCoins;
		}
	}
}
