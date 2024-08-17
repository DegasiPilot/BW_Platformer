using UnityEngine;

namespace BWPlatformer.UI
{
	[RequireComponent(typeof(RectTransform))]
	public class MoveableUI : MonoBehaviour
	{
		private RectTransform _rectTransform;

		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
		}

		public void MoveTo(Vector2 anchoredPosition)
		{
			_rectTransform.anchoredPosition = anchoredPosition;
		}
	}
}
