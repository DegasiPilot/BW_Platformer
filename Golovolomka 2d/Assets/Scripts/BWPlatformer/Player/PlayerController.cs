using BWPlatformer.Input;
using BWPlatformer.Interfaces;
using BWPlatformer.LevelObjects.Bonuses;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BWPlatformer.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
    public class PlayerController : MonoBehaviour, IDamageable, ITeleportable
    {
        public event Action OnJump;
        public event WalkDelegate OnWalk;

        [SerializeField] private float _speed = 5;
        [SerializeField] private float _jumpForce = 7;
        [SerializeField] private float _bonusTime;
        [SerializeField] private Transform _bottomDeathBorder;
		[SerializeField] private BaseInputReader _inputReader;

        private IGroundChecker _groundChecker;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private float _horizontalInput;
		private bool _jumpInput;

        public float Speed { get => _speed; set => _speed = value; }

        public ICollection<TemporaryBonus> ActiveBonuses => _activeBonuses;

        private readonly HashSet<TemporaryBonus> _activeBonuses = new();

		private void Awake()
        {
            _groundChecker = GetComponentInChildren<IGroundChecker>();
			_rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            _inputReader.OnJumpInput += OnJumpInput;
            _inputReader.OnMoveInput += OnMoveInput;
		}

		private void OnMoveInput(Vector2 OnMove)
		{
            _horizontalInput = OnMove.x;
		}

		private void OnJumpInput()
        {
            if (!_jumpInput && Time.timeScale > 0)
            {
                _jumpInput = true;
            }
        }

        void FixedUpdate()
        {
            if (Time.timeScale > 0)
            {
                if (transform.position.y <= _bottomDeathBorder.position.y)
                {
                    Death();
                    return;
                }
                _rigidbody.velocity = new Vector2(_horizontalInput * _speed, _rigidbody.velocity.y);
                bool isGrounded = _groundChecker.IsGrounded;
				if (isGrounded)
				{
					OnWalk.Invoke(Mathf.Abs(_horizontalInput));
				}
				CheckJump(isGrounded);
                Flip();
                JumpAnim(isGrounded);
                RunAnim(isGrounded);
            }
        }

        private void CheckJump(bool isGrounded)
        {
            if (_jumpInput && isGrounded)
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                OnJump?.Invoke();
            }
			_jumpInput = false;
		}

        private void Flip()
        {
            if (_horizontalInput > 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if (_horizontalInput < 0)
            {
                _spriteRenderer.flipX = false;
            }
        }

        private void RunAnim(bool isGrounded)
        {
            if (_horizontalInput != 0 && isGrounded)
            {
                _animator.SetBool("IsRunning", true);
            }
            else
            {
                _animator.SetBool("IsRunning", false);
            }
        }

        private void JumpAnim(bool IsGrounded)
        {
			_animator.SetBool("IsJumping", !IsGrounded);
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

		public void GetDamage()
		{
			Death();
		}

		public void TeleportTo(Vector2 position)
		{
			transform.position = position;
		}
	}

    public delegate void WalkDelegate(float normalizedSpeed);
}