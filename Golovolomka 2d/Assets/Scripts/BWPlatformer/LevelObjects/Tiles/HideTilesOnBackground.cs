using UnityEngine;
using UnityEngine.Tilemaps;

namespace BWPlatformer.LevelObjects.Tiles
{
	public class HideTilesOnBackground : HideOnBackground
	{
		[SerializeField] private Collider2D _myCollider;
		[SerializeField] private TilemapRenderer _renderer;

		protected override void Hide()
		{
			_myCollider.enabled = false;
			_renderer.enabled = false;
		}

		protected override void Show()
		{
			_myCollider.enabled = true;
			_renderer.enabled = true;
		}
	}
}