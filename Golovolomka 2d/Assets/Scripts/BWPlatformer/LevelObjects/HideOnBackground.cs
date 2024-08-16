using UnityEngine;

namespace BWPlatformer.LevelObjects
{
    public class HideOnBackground : MonoBehaviour
    {
        [SerializeField] private Color MyColor;

        public void ChangeState(Color BackColor)
        {
			if (MyColor == BackColor)
			{
				Hide();
			}
			else
			{
				Show();
			}
		}

		protected virtual void Hide()
		{
			gameObject.SetActive(false);
		}

		protected virtual void Show()
		{
			gameObject.SetActive(true);
		}
    }
}