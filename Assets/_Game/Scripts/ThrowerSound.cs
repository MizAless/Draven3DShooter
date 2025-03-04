using UnityEngine;

namespace _Game.Scripts
{
    public class ThrowerSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        public void PlayThrowAxeSound()
        {
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.PlayOneShot(DravenSounds.Instance.ThrowAxeSound);
        }
        
        public void PlayRefillSkillSound()
        {
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.PlayOneShot(DravenSounds.Instance.RefillSkillSound);
        }
    }
}