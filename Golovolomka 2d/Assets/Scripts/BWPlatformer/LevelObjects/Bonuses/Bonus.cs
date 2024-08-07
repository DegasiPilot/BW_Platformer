using UnityEngine;
using BWPlatformer.Enums;
using BWPlatformer.Player;

namespace BWPlatformer.LevelObjects.Bonuses
{
    [RequireComponent(typeof(Collider2D))]
	public abstract class Bonus : MonoBehaviour
    {
		public BonusType BonusType { get; }

        [SerializeField] protected SpriteRenderer _spriteRenderer;

        protected Collider2D _trigger;

        /// <summary>
        /// Initialize _trigger
        /// </summary>
		protected virtual void Awake()
		{
            _trigger = GetComponent<Collider2D>();
		}

		/// <summary>
		/// Invoke OnBonusPicked 
		/// </summary>
		protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerController bonusPickuper))
            {
                OnBonusPicked(bonusPickuper);
                AfterBonusApply();
            }
        }

		/// <summary>
		/// Deactivate _spriteRenderer (if it not null) and _trigger
		/// </summary>
		protected virtual void OnBonusPicked(PlayerController bonusPickuper)
        {
            if(_trigger == null)
            {
				_trigger = GetComponent<Collider2D>();
			}
            _trigger.enabled = false;
            if(_spriteRenderer != null)
            {
                _spriteRenderer.enabled = false;
            }
        }

        /// <summary>
        /// By default destroy gameObject
        /// </summary>
        protected virtual void AfterBonusApply()
        {
            Destroy(gameObject);
        }
	}
}