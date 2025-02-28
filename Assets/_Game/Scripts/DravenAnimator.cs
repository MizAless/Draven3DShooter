using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class DravenAnimator : MonoBehaviour
    {
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int MoveDirection = Animator.StringToHash("MoveDirection");
        
        private Animator _animator;
        private Mover _mover;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mover = GetComponent<Mover>();
        }

        private void OnEnable()
        {
            _mover.Moved += OnMoved;
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