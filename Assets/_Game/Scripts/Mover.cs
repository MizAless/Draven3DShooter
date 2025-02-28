using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class Mover : MonoBehaviour
    {
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int MoveDirection = Animator.StringToHash("MoveDirection");

        [SerializeField] private float _speed;

        public event Action<float> Moved;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            // _animator.SetBool(IsRunning, false);

            Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

            if (moveDirection == Vector3.zero)
                return;

            
            // _animator.SetFloat(MoveDirection, vertical >= 0 ? 1 : -1);
            // _animator.SetBool(IsRunning, true);

            moveDirection.Normalize();
            transform.position += moveDirection * (_speed * Time.deltaTime);
            
            Moved?.Invoke(vertical >= 0 ? 1 : -1);
        }
    }
}