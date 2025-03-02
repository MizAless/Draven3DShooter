using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public event Action<float> Moved;

        public Vector2 MoveDirection { get; private set; }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            MoveDirection = new Vector2(horizontal, vertical);

            if (MoveDirection == Vector2.zero)
                return;

            Debug.DrawRay(transform.position + Vector3.up * 1.5f, new Vector3(MoveDirection.x, 0, MoveDirection.y) * _speed, Color.red);
            MoveDirection.Normalize();
            transform.position += transform.forward * (MoveDirection.y * _speed * Time.deltaTime);
            transform.position += transform.right * (MoveDirection.x * _speed * Time.deltaTime);

            Moved?.Invoke(vertical >= 0 ? 1 : -1);
        }
    }
}