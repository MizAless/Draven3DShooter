using UnityEngine;

namespace _Game.Scripts
{
    public class AxeAmmunitionSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSourceL;
        [SerializeField] private AudioSource _audioSourceR;
        
        private AxeAmmunition _axeAmmunition;
        
        private CycledSound _LSpin;
        private CycledSound _RSpin;

        private void Start()
        {
            _axeAmmunition = GetComponent<AxeAmmunition>();
            var spinSound = DravenSounds.Instance.SpinAxeSound;
            
            _LSpin = new CycledSound(_audioSourceL, spinSound);
            _RSpin = new CycledSound(_audioSourceR, spinSound);
            
            _axeAmmunition.Changed += OnChanged;
        }

        // private void OnEnable()
        // {
            // _axeAmmunition.Changed += OnChanged;
        // }

        private void OnDisable()
        {
            _axeAmmunition.Changed -= OnChanged;
        }

        private void OnChanged()
        {
            if (_axeAmmunition.CurrentCount == 2)
            {
                _RSpin.Play(this);
            }
            else if (_axeAmmunition.CurrentCount == 1)
            {
                _LSpin.Play(this);
                _RSpin.Stop();
            }
            else if (_axeAmmunition.CurrentCount == 0)
            {
                _LSpin.Stop();
                _RSpin.Stop();
            }
        }
    }
}