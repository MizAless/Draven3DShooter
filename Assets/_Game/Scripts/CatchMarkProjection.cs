using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class CatchMarkProjection : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private List<GameObject> _elements = new List<GameObject>();
        [SerializeField] private float _maxWidth = 800f;
        [SerializeField] private float _minWidth = 100f;
        [SerializeField] private float _maxDistance = 2f;

        [SerializeField] private RectMask2D _effectMask;
        
        private Mover _mover;
        private CatchTrigger _mark;
        private RectTransform _rectTransform;

        public static CatchMarkProjection Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            SetActiveVisual(false);

            // _image.color = _color1;
            // _image.DOColor(_color2, _blinkDuration)
            //     .SetLoops(-1, LoopType.Yoyo) // Бесконечный цикл с переключением между цветами
            //     .SetEase(Ease.Linear); // Линейное изменение цвета
        }

        public void SetMark(CatchTrigger mark)
        {
            _mark = mark;
        }

        public void SetMover(Mover mover)
        {
            _mover = mover;
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

            UpdatePosition(screenPos);
            UpdateWidth();
        }

        private void UpdatePosition(Vector3 screenPos)
        {
            Vector3 screenPixelPos = new Vector3(screenPos.x * Screen.width, screenPos.y * Screen.height, 0);
            Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Vector3 direction = screenPixelPos - center;
            Vector3 directionNormalized = direction.normalized;
            Vector3 edgePosition = CalculateEdgePosition(center, directionNormalized);

            int rotationCoeff = edgePosition.x > 0 ? 1 : -1;

            if (Mathf.Approximately(Mathf.Abs(edgePosition.x), Screen.width / 2))
            {
                _rectTransform.rotation = Quaternion.Euler(0, 0, rotationCoeff * 90);
                _effectMask.enabled = false;
            }
            else
            {
                _rectTransform.rotation = Quaternion.Euler(0, 0, 0);
                _effectMask.enabled = true;
            }

            if (screenPos.z < 0)
                edgePosition *= -1;

            _rectTransform.anchoredPosition = edgePosition;
        }

        private Vector3 CalculateEdgePosition(Vector3 center, Vector3 directionNormalized)
        {
            float maxX = Screen.width;
            float maxY = Screen.height;

            float edgeX = Mathf.Clamp(center.x + directionNormalized.x * maxX, 0, maxX);
            float edgeY = Mathf.Clamp(center.y + directionNormalized.y * maxY, 0, maxY);

            // Учитываем pivot в центре Canvas
            return new Vector3(edgeX - Screen.width / 2, edgeY - Screen.height / 2, 0);
        }

        private void UpdateWidth()
        {
            float distance = Vector3.Distance(_mark.transform.position, _mover.transform.position);
            float normalizedDistance = Mathf.Clamp01(distance / _maxDistance);
            float width = Mathf.Lerp(_minWidth, _maxWidth, normalizedDistance);

            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }

        private void SetActiveVisual(bool isActive)
        {
            foreach (var element in _elements)
            {
                element.SetActive(isActive);
            }
        }
    }
}