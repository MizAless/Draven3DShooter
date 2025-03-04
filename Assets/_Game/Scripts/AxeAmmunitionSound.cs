using UnityEngine;

namespace _Game.Scripts
{
    public class AxeAmmunitionSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSourceL;
        [SerializeField] private AudioSource _audioSourceR;
        
        private AxeAmmunition _axeAmmunition;


        private void Awake()
        {
            _axeAmmunition = GetComponent<AxeAmmunition>();
            
            _audioSourceR.loop = true;
            _audioSourceL.loop = true;
        }

        private void OnEnable()
        {
            _axeAmmunition.Changed += OnChanged;
        }

        private void OnDisable()
        {
            _axeAmmunition.Changed -= OnChanged;
        }

        private void OnChanged()
        {
            if (_axeAmmunition.CurrentCount == 2)
            {
                _audioSourceR.Play();
            }
            else if (_axeAmmunition.CurrentCount == 1)
            {
                _audioSourceL.Play();
                _audioSourceR.Stop();
            }
            else if (_axeAmmunition.CurrentCount == 0)
            {
                _audioSourceL.Stop();
                _audioSourceR.Stop();
            }
        }
    }
}