using System.Collections;
using UnityEngine;

namespace _Game.Scripts
{
    public class CatchTrigger : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _catchEffect;
        [SerializeField] private float _destroyDelay;
        
        private bool _haveCatcher = false;

        public void Activate()
        {
            StartCoroutine(ActivateCoroutine());
        }
        
        private IEnumerator ActivateCoroutine()
        {
            if (_haveCatcher)
            {
                var catchEffectMain = _catchEffect.main;
                catchEffectMain.duration = _destroyDelay;
                _catchEffect.Play();
                
                yield return new WaitForSeconds(_destroyDelay);
            }

            OnEnd();
        }
        
        private void OnEnd()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out AxeCatcher _))
                return;

            _haveCatcher = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_haveCatcher)
                return;
            
            if (!other.gameObject.TryGetComponent(out AxeCatcher _))
                return;

            _haveCatcher = false;
        }
    }
}