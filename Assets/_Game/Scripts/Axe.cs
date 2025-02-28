using System;
using System.Collections;
using System.Diagnostics.Tracing;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

namespace _Game.Scripts
{
    public class Axe : MonoBehaviour
    {
        [SerializeField] private Transform _model;

        [Header("Bounce")] 
        [SerializeField] private AnimationCurve _bounceXTrajectory;
        [SerializeField] private AnimationCurve _bounceYTrajectory;
        [SerializeField] private float _bounceDuration;
        [SerializeField] private float _bounceHight;

        [SerializeField] private CatchTrigger _catchTrigger;

        private Rigidbody _rigidbody;
        private Thrower _thrower;

        private bool _canInteract = true;

        public event Action Catched;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(Thrower thrower)
        {
            _thrower = thrower;
        }

        public void Launch(Vector3 direction, float force)
        {
            StartRotate();
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }

        private void StartRotate(bool toForward = true)
        {
            _model.DOKill();

            int coefficient = toForward ? 1 : -1;

            _model.DOLocalRotate(new Vector3(360 * coefficient, 0, 0), 0.2f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!_canInteract)
                return;

            if (!other.gameObject.TryGetComponent(out Target _))
            {
                Destroy(gameObject);
                return;
            }
                
            _canInteract = false;
            BounceToPoint();
        }

        private void BounceToPoint()
        {
            StartRotate(false);

            GetComponent<Collider>().isTrigger = true;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.isKinematic = true;

            var endPoint = GetRandomPoint();

            var catchTrigger = Instantiate(_catchTrigger, endPoint, Quaternion.identity);

            StartCoroutine(BounceCoroutine(transform.position, endPoint, () => catchTrigger.Activate()));
        }

        private IEnumerator BounceCoroutine(Vector3 startPoint, Vector3 targetPoint, Action callback = null)
        {
            float progress = 0;

            while (progress <= 1)
            {
                transform.position = Vector3.Lerp(startPoint, targetPoint, _bounceXTrajectory.Evaluate(progress));
                transform.position += Vector3.up * (_bounceYTrajectory.Evaluate(progress) * _bounceHight);
                progress += Time.deltaTime / _bounceDuration;
                yield return null;
            }

            transform.position = Vector3.Lerp(startPoint, targetPoint, _bounceXTrajectory.Evaluate(1));
            transform.position += Vector3.up * (_bounceYTrajectory.Evaluate(1) * _bounceHight);

            Destroy(gameObject);
            
            callback?.Invoke();
        }

        private Vector3 GetRandomPoint()
        {
            var randomVector = Random.insideUnitCircle;
            var direction = new Vector3(randomVector.x, 0, randomVector.y) * 0f;

            return _thrower.transform.position + direction;
        }
    }
}