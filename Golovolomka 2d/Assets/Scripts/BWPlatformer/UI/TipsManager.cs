using System.Collections.Generic;
using UnityEngine;

namespace BWPlatformer.UI
{
	[Tooltip("Detect all tips on children")]
	public class TipsManager : MonoBehaviour
	{
		private List<TipOnUI> _tipsOnUI;

		private void Awake()
		{
			var tips = GetComponentsInChildren<TipOnUI>();
			_tipsOnUI = new(tips.Length);
			DeviceType deviceType = SystemInfo.deviceType;
			foreach(var tip in tips)
			{
				if(tip.TargetDeviceType == DeviceType.Unknown || tip.TargetDeviceType == deviceType)
				{
					_tipsOnUI.Add(tip);
					tip.Init();
				}
				else
				{
					Destroy(tip.gameObject);
				}
			}
		}

		public void ChangeColorForTips(Color backgroundColor)
		{
			Color newColor = backgroundColor == Color.black ? Color.white : Color.black;
			foreach (var tip in _tipsOnUI)
			{
				tip.ChangeColor(newColor);
			}
		}

		public void DestroyTip(TipOnUI tip)
		{
			_tipsOnUI.Remove(tip);
			Destroy(tip.gameObject);
		}
	}
}
