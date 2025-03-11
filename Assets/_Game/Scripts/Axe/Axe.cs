using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace _Game.Scripts
{
    public class Axe : MonoBehaviour
    {
        [SerializeField] private Transform _model;

        [Header("Bounce")] [SerializeField] private AnimationCurve _bounceXTrajectory;
        [SerializeField] private AnimationCurve _bounceYTrajectory;
        [SerializeField] private float _bounceDuration;
        [SerializeField] private float _bounceHight;
        [SerializeField] private float _maxRandomBounceOffset;
        [SerializeField] private float _moveBounceOffset;
        [Space] [SerializeField] private CatchTrigger _catchTrigger;
        [Space] [SerializeField] private DisappearingAxe _disappearingAxePrefab;

        private Rigidbody _rigidbody;
        private Mover _mover;
        private AxeSound _axeSound;

        private bool _canInteract = true;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _axeSound = GetComponent<AxeSound>();
        }

        public void Init(Mover mover)
        {
            _mover = mover;
        }

        public void Launch(Vector3 direction, float force)
        {
            StartRotate();
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
            _axeSound.PlaySpinAxeSound();
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
            
            _axeSound.PlayAxeHitSound();

            var contact = other.contacts[0];
            
            if (!other.gameObject.TryGetComponent(out Target target))
            {
                CreateDisappearingAxe(contact.point, contact.normal);
                Destroy(gameObject);
                return;
            }

            EffectsCreator.Instance.CreateEmbers(contact.point);
            target.Hit();
            _canInteract = false;
            BounceToPoint();
        }

        private void BounceToPoint()
        {
            StartRotate(false);
            SetAsTrigger();
            var endPoint = GetBouncePoint();
            Vector3 lookDirection = transform.position - endPoint;
            LookAtTween(lookDirection);

            var catchTrigger = Instantiate(_catchTrigger, endPoint, Quaternion.identity);
            catchTrigger.Catched += AxeAmmunition.Instance.Add;
            catchTrigger.Missed += CreateDisappearingAxe;
            CatchMarkRadial.Instance.SetMark(catchTrigger);

            StartCoroutine(BounceCoroutine(transform.position, endPoint, () => catchTrigger.Activate()));
        }

        private void LookAtTween(Vector3 lookDirection)
        {
            Quaternion endRotation = Quaternion.LookRotation(lookDirection.normalized);
            endRotation.eulerAngles = new Vector3(0, endRotation.eulerAngles.y, 0);

            transform
                .DORotateQuaternion(endRotation, 0.2f)
                .SetEase(Ease.InOutQuad);
        }

        private void SetAsTrigger()
        {
            GetComponent<Collider>().isTrigger = true;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.isKinematic = true;
        }

        private void CreateDisappearingAxe()
        {
            CreateDisappearingAxe(transform.position, Vector3.up);
        }

        private void CreateDisappearingAxe(Vector3 position, Vector3 normal)
        {
            var disappearingAxe = Instantiate(_disappearingAxePrefab, position, transform.rotation);
            disappearingAxe.Init(normal);
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

        private Vector3 GetBouncePoint()
        {
            if (_mover.MoveDirection != Vector3.zero)
                return _mover.transform.position + _mover.MoveDirection * _moveBounceOffset;

            var randomVector = Random.insideUnitCircle;
            var direction = new Vector3(randomVector.x, 0, randomVector.y) * _maxRandomBounceOffset;

            return _mover.transform.position + direction;
        }
    }
}