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
        
        public static DravenSounds Instance { get; private set; }

        private void Start()
        {
            Instance = this;
        }
        
        public void PlayRefillSkillSound()
        {
            
        }
        
        public void PlayThrowAxeSound()
        {
            
        }
        
        public void PlaySpinAxeSound()
        {
            
        }
        
        public void PlayAxeHitSound()
        {
            
        }
        
        public void PlayCatchAxeSound()
        {
            
        }
        
        public void PlayAxeFlyAfterBounceSound()
        {
            
        }
    }
}