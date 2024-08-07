using UnityEngine;

namespace BWPlatformer.LevelObjects.Tiles
{
    public class HideOnBackground : MonoBehaviour
    {
        public Color MyColor;

        public void ChangeState(Color BackColor)
        {
			if (MyColor == BackColor)
			{
				gameObject.SetActive(false);
			}
			else
			{
				gameObject.SetActive(true);
			}
		}
    }
}