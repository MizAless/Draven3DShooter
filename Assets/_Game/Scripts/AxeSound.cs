using System.Collections;
using UnityEngine;

namespace _Game.Scripts
{
    public class AxeSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        public void PlaySpinAxeSound()
        {
            // _audioSource.clip = DravenSounds.Instance.SpinAxeSound;
            // _audioSource.loop = true;
            // _audioSource.Play();

            StartCoroutine(SpinSoundCoroutine());
        }
        
        private IEnumerator SpinSoundCoroutine()
        {
            while (enabled)
            {
                _audioSource.PlayOneShot(DravenSounds.Instance.SpinAxeSound);
                yield return new WaitForSeconds(1f);
            }
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