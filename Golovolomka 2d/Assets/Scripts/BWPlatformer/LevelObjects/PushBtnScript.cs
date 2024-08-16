using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace BWPlatformer.LevelObjects
{
    [RequireComponent(typeof(Animator), typeof(Collider2D))]
    public class PushBtnScript : MonoBehaviour
    {
        public UnityEvent OnPressed;

        private Animator _animator;
        private Collider2D _myCollider;
        private bool _isPressed;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _myCollider = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!_isPressed)
            {
                StopAllCoroutines();
                _isPressed = true;
                _animator.SetBool("IsPressed", _isPressed);
                OnPressed.Invoke();
                StartCoroutine(CheckButtonState(collision.collider));
            }
        }

        IEnumerator CheckButtonState(Collider2D other)
        {
            yield return new WaitForFixedUpdate();
            while (_isPressed)
            {
                if (_myCollider.IsTouching(other))
                {
                    yield return new WaitForFixedUpdate();
                }
                else
                {
                    _isPressed = false;
                    _animator.SetBool("IsPressed", _isPressed);
                }
            }
        }
    }
}