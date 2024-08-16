using UnityEngine;
using UnityEngine.UI;

namespace BWPlatformer.UI
{
	[RequireComponent(typeof(Text))]
	public class TipOnUI : MonoBehaviour
	{
		[Tooltip("Use Unknown to show this tip on all devices")]
		[SerializeField] private DeviceType _targetDeviceType;
		public DeviceType TargetDeviceType => _targetDeviceType;

		private Text _text;

		public void Init()
		{
			_text = GetComponent<Text>();
		}

		public void ChangeColor(Color newColor)
		{
			_text.color = newColor;
		}
	}
}