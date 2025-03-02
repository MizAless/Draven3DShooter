using UnityEngine;

namespace _Game.Scripts
{
    public class DummyAnimatorController : MonoBehaviour
    {
        private static readonly int IsHited = Animator.StringToHash("IsHited");
        
        private Animator _animator;
        private Target _target;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _target = GetComponent<Target>();
        }

        private void OnEnable()
        {
            _target.Hited += OnHited;
        }

        private void OnDisable()
        {
            _target.Hited -= OnHited;
        }

        private void OnHited()
        {
            _animator.SetTrigger(IsHited);
        }
    }
}