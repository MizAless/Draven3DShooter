using UnityEngine;

namespace _Game.Scripts
{
    public class AxeSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        public void PlaySpinAxeSound()
        {
            _audioSource.clip = DravenSounds.Instance.SpinAxeSound;
            _audioSource.loop = true;
            _audioSource.Play();
        }
        
        public void PlayAxeHitSound()
        {
            _audioSource.PlayOneShot(DravenSounds.Instance.AxeHitSound);
        }
        
        public void PlayAxeFlyAfterBounceSound()
        {
            _audioSource.PlayOneShot(DravenSounds.Instance.AxeFlyAfterBounceSound);
        }
    }
}