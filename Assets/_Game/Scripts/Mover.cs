using UnityEngine;

namespace _Game.Scripts
{
    public class Mover : MonoBehaviour
    {
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int MoveDirection = Animator.StringToHash("MoveDirection");

        [SerializeField] private float _speed;

        private Animator _animator;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            HandleInput();
            HandleJump();
        }

        private void HandleInput()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            _animator.SetBool(IsRunning, false);

            Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

            if (moveDirection == Vector3.zero)
                return;

            
            
            _animator.SetFloat(MoveDirection, vertical >= 0 ? 1 : -1);
            _animator.SetBool(IsRunning, true);

            moveDirection.Normalize();

            transform.position += moveDirection * (_speed * Time.deltaTime);
        }
        
        private void HandleJump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _rigidbody.AddForce(Vector3.up * 1000);
        }
    }
}