using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BWPlatformer.Input
{
	[RequireComponent(typeof(RectTransform))]
	public class FloatingStickEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		[SerializeField] private UnityEvent<Vector2> OnStickPointDown;
		[SerializeField] private UnityEvent<Vector2> OnStickPointUp;

		private RectTransform _rectTransform;
		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			OnStickPointDown.Invoke(_rectTransform.anchoredPosition);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			OnStickPointUp.Invoke(_rectTransform.anchoredPosition);
		}
	}
}
