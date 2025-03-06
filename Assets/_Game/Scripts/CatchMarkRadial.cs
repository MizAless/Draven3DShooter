using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class CatchMarkRadial : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _view;

        [Header("Width")] [SerializeField] private Slider _widthSlider;
        [SerializeField, Range(0, 1)] private float _maxWidth = 0.3f;
        [SerializeField, Range(0, 1)] private float _minWidth = 0.1f;
        [SerializeField] private float _maxDistance = 2f;


        private Mover _mover;
        private CatchTrigger _mark;
        private float _angleWidthOffset = 0;

        public static CatchMarkRadial Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (_mark == null || _mover == null)
            {
                SetActiveVisual(false);
                return;
            }

            Vector3 screenPos = _camera.WorldToViewportPoint(_mark.transform.position);
            bool isVisible = screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1 &&
                             screenPos.z > 0;

            SetActiveVisual(!isVisible);

            if (isVisible)
                return;

            UpdateRotation();
            UpdateWidth();
        }

        public void SetMark(CatchTrigger mark)
        {
            _mark = mark;
        }

        public void SetMover(Mover mover)
        {
            _mover = mover;
        }

        private void UpdateRotation()
        {
            var direction = _mover.transform.InverseTransformPoint(_mark.transform.position);
            direction.y = 0;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.x, direction.z);
            float angleDegrees = angle * Mathf.Rad2Deg;

            angleDegrees = 180 - angleDegrees;
            angleDegrees += _angleWidthOffset;

            transform.rotation = Quaternion.Euler(0, 0, angleDegrees);
        }

        private void UpdateWidth()
        {
            float distance = Vector3.Distance(_mark.transform.position, _mover.transform.position);
            float normalizedDistance = Mathf.Clamp01(distance / _maxDistance);
            float impactCoefficient = 1 - normalizedDistance;
            float width = Mathf.Lerp(_minWidth, _maxWidth, impactCoefficient);
            _angleWidthOffset = width * 180f;
            _widthSlider.value = width;
        }

        private void SetActiveVisual(bool isActive)
        {
            _view.SetActive(isActive);
        }
    }
}