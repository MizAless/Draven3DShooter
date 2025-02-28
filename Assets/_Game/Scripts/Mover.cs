using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public event Action<float> Moved;

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

            if (moveDirection == Vector3.zero)
                return;

            moveDirection.Normalize();
            transform.position += transform.forward * (moveDirection.z * _speed * Time.deltaTime);
            transform.position += transform.right * (moveDirection.x * _speed * Time.deltaTime);
            
            Moved?.Invoke(vertical >= 0 ? 1 : -1);
        }
    }
}