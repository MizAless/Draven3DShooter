using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class CatchMarkProjection : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private List<GameObject> _elements = new List<GameObject>();

        private Mover _mover;
        private CatchTrigger _mark;
        private RectTransform _rectTransform;

        public static CatchMarkProjection Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _rectTransform = GetComponent<RectTransform>(); // Получаем RectTransform элемента Canvas
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
            if (_mark == null)
                return;

            Vector3 screenPos = _camera.WorldToViewportPoint(_mark.transform.position);
            bool isVisible = screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1 &&
                             screenPos.z > 0;

            SetActiveVisual(!isVisible);

            print( screenPos);
            
            if (isVisible)
                return;

            Vector3 screenPixelPos = new Vector3(screenPos.x * Screen.width, screenPos.y * Screen.height, 0);
            Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Vector3 direction = screenPixelPos - center;
            Vector3 directionNormalized = direction.normalized;
            Vector3 edgePosition = CalculateEdgePosition(center, directionNormalized);

            int rotationCoeff = edgePosition.x > 0 ? 1 : -1;
            
            if (Mathf.Approximately(Mathf.Abs(edgePosition.x), Screen.width / 2))
                _rectTransform.rotation = Quaternion.Euler(0, 0, rotationCoeff * 90);
            else
                _rectTransform.rotation = Quaternion.Euler(0, 0, 0);

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

            return new Vector3(edgeX - Screen.width / 2, edgeY - Screen.height / 2, 0); // Учитываем pivot в центре Canvas
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