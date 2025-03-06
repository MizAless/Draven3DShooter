using UnityEngine;

namespace _Game.Scripts
{
    public class DravenSounds : MonoBehaviour
    {
        [field: SerializeField] public AudioClip RefillSkillSound;
        [field: SerializeField] public AudioClip ThrowAxeSound;
        [field: SerializeField] public AudioClip SpinAxeSound;
        [field: SerializeField] public AudioClip AxeHitSound;
        [field: SerializeField] public AudioClip CatchAxeSound;
        [field: SerializeField] public AudioClip AxeFlyAfterBounceSound;
        
        [SerializeField] private AudioSource _audioSource;
        
        public static DravenSounds Instance { get; private set; }

        private void Start()
        {
            Instance = this;
        }
        
        public void PlayCatchAxeSound()
        {
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.PlayOneShot(DravenSounds.Instance.CatchAxeSound);
        }
    }
}