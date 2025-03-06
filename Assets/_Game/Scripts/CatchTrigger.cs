using System;
using System.Collections;
using UnityEngine;

namespace _Game.Scripts
{
    public class CatchTrigger : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _catchEffect;
        [SerializeField] private float _destroyDelay;
        
        private CatchTriggerSound _catchTriggerSound;
        
        private bool _haveCatcher = false;
        public event Action Catched;
        public event Action Missed;

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

                DravenSounds.Instance.PlayCatchAxeSound();
                
                Catched?.Invoke();

                yield return new WaitForSeconds(_destroyDelay);
            }
            else
                Missed?.Invoke();

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