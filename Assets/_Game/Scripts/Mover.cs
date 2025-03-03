using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public event Action<float> Moved;

        public Vector3 MoveDirection { get; private set; }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            MoveDirection = transform.rotation * new Vector3(horizontal, 0, vertical);

            if (MoveDirection == Vector3.zero)
                return;

            MoveDirection.Normalize();

            Debug.DrawRay(transform.position + Vector3.up * 1.5f,
                MoveDirection, Color.red);

            transform.position += MoveDirection * (_speed * Time.deltaTime);

            Moved?.Invoke(vertical >= 0 ? 1 : -1);
        }
    }
}