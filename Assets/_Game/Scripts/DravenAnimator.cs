using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class DravenAnimator : MonoBehaviour
    {
        private static readonly int IsRunning = Animator.StringToHash(nameof(IsRunning));
        private static readonly int MoveDirection = Animator.StringToHash(nameof(MoveDirection));
        private static readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
        
        private Animator _animator;
        private Mover _mover;
        private Thrower _thrower;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mover = GetComponent<Mover>();
            _thrower = GetComponent<Thrower>();
        }

        private void OnEnable()
        {
            _mover.Moved += OnMoved;
            _thrower.Throwed += OnThrowed;
        }

        private void OnDisable()
        {
            _mover.Moved += OnMoved;
            _thrower.Throwed += OnThrowed;
        }

        private void OnThrowed()
        {
            _animator.SetTrigger(IsAttacking);
        }

        private void Update()
        {
            _animator.SetBool(IsRunning, false);
        }

        private void OnMoved(float moveDirection)
        {
            _animator.SetFloat(MoveDirection, moveDirection);
            _animator.SetBool(IsRunning, true);
        }
    }
}