using System;
using UnityEngine;
using DG.Tweening;

namespace _Game.Scripts
{
    public class Axe : MonoBehaviour
    {
        [SerializeField] private Transform _model;
        
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(Vector3 direction, float force)
        {
            StartRotate();
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }

        private void StartRotate()
        {
            _model.DORotate(new Vector3(360, 0, 0), 0.2f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }
    }
}