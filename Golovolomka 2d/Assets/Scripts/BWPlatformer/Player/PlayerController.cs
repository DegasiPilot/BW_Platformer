using BWPlatformer.Enums;
using BWPlatformer.Interfaces;
using BWPlatformer.LevelObjects.Bonuses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BWPlatformer.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5;
        [SerializeField] private float _jumpForce = 7;
        [SerializeField] private KeyCode _jumpButton;
        [SerializeField] private float _bonusTime;
        [SerializeField] private Transform _bottomDeathBorder;

        private IGroundChecker _groundChecker;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private BonusType _currentBonus = BonusType.None;
        private Coroutine _bonusCourotine;

        public float Speed { get => _speed; set => _speed = value; }

        public ICollection<TemporaryBonus> ActiveBonuses => _activeBonuses;

        private readonly HashSet<TemporaryBonus> _activeBonuses = new();

		private void Awake()
        {
            _groundChecker = GetComponentInChildren<IGroundChecker>();
			_rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

			_groundChecker.OnLanded += () => _animator.SetBool("IsJumping", false);
			_groundChecker.OnJumped += () => _animator.SetBool("IsJumping", true);
		}

        void FixedUpdate()
        {
            if(transform.position.y <= _bottomDeathBorder.position.y)
            {
                Death();
                return;
            }
            float h = Input.GetAxis("Horizontal");
            _rigidbody.velocity = new Vector2(h * _speed, _rigidbody.velocity.y);
            Flip(h);
            RunAnim(h);
        }

        private void Update()
        {
            if (Input.GetKeyDown(_jumpButton) && _groundChecker.IsGrounded)
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }

        private void Flip(float h)
        {
            if (h > 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if (h < 0)
            {
                _spriteRenderer.flipX = false;
            }
        }

        private void RunAnim(float h)
        {
            if (h != 0 && _groundChecker.IsGrounded)
            {
                _animator.SetBool("IsRunning", true);
            }
            else
            {
                _animator.SetBool("IsRunning", false);
            }
        }

        public void Death()
        {
            _rigidbody.velocity = new Vector2(0, 0);
            _rigidbody.simulated = false;
            _spriteRenderer.enabled = false;
            StartCoroutine(Respawn());
        }

        private IEnumerator Respawn()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		}

        internal void PickUpBonus(BonusType bonusType)
        {
            if(_bonusCourotine != null) 
            { 
                StopCoroutine(_bonusCourotine); 
            }
            _currentBonus = bonusType;
            if (_currentBonus == BonusType.Slow)
            {
                _speed = 3;
            }
            else if (_currentBonus == BonusType.SpeedUp)
            {
                _speed = 6.5f;
            }
            _bonusCourotine = StartCoroutine(BonusTimer());
        }

        private IEnumerator BonusTimer()
        {
            yield return new WaitForSeconds(_bonusTime);
            _currentBonus = BonusType.None;
            _speed = 5;
        }
    }
}