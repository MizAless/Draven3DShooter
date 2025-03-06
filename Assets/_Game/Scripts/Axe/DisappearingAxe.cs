using UnityEngine;
using DG.Tweening;


namespace _Game.Scripts
{
    public class DisappearingAxe : MonoBehaviour
    {
        [SerializeField] private GameObject _model;
        [SerializeField] private float _maxOffsetAngle;
        [SerializeField] private float _fadeDuration;

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = _model.GetComponent<Renderer>();
        }

        public void Init(Vector3 insertionNormal)
        {
            var randomRotationX = Random.Range(-_maxOffsetAngle, _maxOffsetAngle);
            var rotationY = transform.rotation.eulerAngles.y;
            var randomRotationZ = Random.Range(-_maxOffsetAngle, _maxOffsetAngle);

            var rotation = Quaternion.Euler(randomRotationX, rotationY, randomRotationZ);
            transform.rotation = rotation;

            Disappear();
        }

        private void Disappear()
        {
            _renderer.material
                .DOFade(0, _fadeDuration)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}