using UnityEngine;

namespace _Game.Scripts
{
    public class CatchTriggerSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        public void PlayCatchAxeSound()
        {
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.PlayOneShot(DravenSounds.Instance.CatchAxeSound);
        }
    }
}