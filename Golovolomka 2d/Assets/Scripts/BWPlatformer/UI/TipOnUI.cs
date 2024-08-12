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

		private void Start()
		{
			_text = GetComponent<Text>();
		}

		public void ChangeColor(Color backgroundColor)
		{
			_text.color = backgroundColor == Color.black ? Color.white : Color.black;
		}
	}
}