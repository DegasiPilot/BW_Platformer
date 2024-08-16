using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BWPlatformer.LevelObjects.Tiles
{
	[RequireComponent(typeof(Tilemap))]
	public class BrokeTilesUnderPlayer : BrokeUnderPlayer
	{
		[SerializeField] bool _brokeAllTilesTogether;

		private readonly Vector3Int[] _directions = { Vector3Int.up, Vector3Int.right, Vector3Int.down, Vector3Int.left };

		[SerializeField] AnimatedTile _brokingAnimation;

		private Tilemap _tilemap;
		private Grid _grid;
		private readonly HashSet<Vector3Int> _allTilesToBroke = new();

		private readonly ContactPoint2D[] _contacts = new ContactPoint2D[4];
		private Func<TileBase, Vector3Int, IEnumerator> brokeRoutine;


		protected override void Awake()
		{
			base.Awake();
			_tilemap = GetComponent<Tilemap>();
			_grid = _tilemap.layoutGrid;
			if (_brokeAllTilesTogether)
			{
				brokeRoutine = BrokeTogether;
			}
			else
			{
				brokeRoutine = BrokeSeparately;
			}
		}

		protected override void Broke(Collision2D collision)
		{
			int contactsCount = collision.GetContacts(_contacts);
			for(int i = 0; i < contactsCount; i++)
			{
				Vector2 dotInTile = _contacts[i].point + _contacts[i].normal/2;
				Vector3Int tilePos = _grid.WorldToCell(dotInTile);
				TileBase tile = _tilemap.GetTile(tilePos);
				if (tile != null && !_allTilesToBroke.Contains(tilePos))
				{
					StartCoroutine(brokeRoutine(tile, tilePos));
				}
			}
		}

		private IEnumerator BrokeSeparately(TileBase tileType, Vector3Int startTilePos)
		{
			foreach (var tilesGroup in GetBrokingTiles(tileType, startTilePos))
			{
				_allTilesToBroke.UnionWith(tilesGroup);
				yield return BrokeTilesInstruction(tilesGroup);
				_allTilesToBroke.ExceptWith(tilesGroup);
			}
			yield break;
		}

		private IEnumerator BrokeTogether(TileBase tileType, Vector3Int startTilePos)
		{
			HashSet<Vector3Int> tilesToBroke = new();
			foreach (var tilesGroup in GetBrokingTiles(tileType, startTilePos))
			{
				_allTilesToBroke.UnionWith(tilesGroup);
				tilesToBroke.UnionWith(tilesGroup);
			}
			yield return BrokeTilesInstruction(tilesToBroke.ToArray());
			_allTilesToBroke.ExceptWith(tilesToBroke);
			yield break;
		}

		private IEnumerator BrokeTilesInstruction(Vector3Int[] tilesToBroke)
		{
			if (_brokingAnimation != null)
			{
				_tilemap.SetTiles(tilesToBroke, Enumerable.Repeat(_brokingAnimation, tilesToBroke.Length).ToArray());
			}
			yield return _waitInstruction;
			_tilemap.SetTiles(tilesToBroke, Enumerable.Repeat<TileBase>(null, tilesToBroke.Length).ToArray());
		}

		/// <summary>
		/// Check tiles only in _allTilesToBroke
		/// </summary>
		private IEnumerable<Vector3Int[]> GetBrokingTiles(TileBase tileType, Vector3Int startTile)
		{
			yield return new[] {startTile};

			HashSet<Vector3Int> outerTiles = new() { startTile };
			HashSet<Vector3Int> newTiles = new();

			for (int distance = 1; outerTiles.Count > 0; distance++)
			{
				foreach (var tilePos in outerTiles)
				{
					foreach (var direction in _directions)
					{
						var neighbour = tilePos + direction;
						if (IsExistTile(tileType, neighbour) && !_allTilesToBroke.Contains(neighbour))
						{
							newTiles.Add(neighbour);
						}
					}
				}
				outerTiles.Clear();
				outerTiles.UnionWith(newTiles);
				yield return newTiles.ToArray();
				newTiles.Clear();
			}
			yield break;
		}

		private bool IsExistTile(TileBase tileType, Vector3Int tilePos)
		{
			return _tilemap.GetTile(tilePos) == tileType;
		}
	}
}