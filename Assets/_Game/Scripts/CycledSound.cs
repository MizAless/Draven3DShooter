using System.Collections;
using UnityEngine;

namespace _Game.Scripts
{
    public class CycledSound
    {
        private readonly AudioSource _audioSource;
        private readonly AudioClip _audioClip;
        
        private bool _isPlaying;
        
        private Coroutine _soundCoroutine;

        public CycledSound(AudioSource audioSource, AudioClip audioClip)
        {
            _audioSource = audioSource;
            _audioClip = audioClip;
        }

        public void Play(MonoBehaviour coroutineOwner)
        {
            if (_isPlaying)
                return;
            
            _soundCoroutine = coroutineOwner.StartCoroutine(PlaySound());
        }

        public void Stop()
        {
            _isPlaying = false; 
        }

        private IEnumerator PlaySound()
        {
            _isPlaying = true;

            var delay = new WaitForSeconds(_audioClip.length - 0.06f);
            
            while (_isPlaying)
            {
                _audioSource.PlayOneShot(_audioClip);
                yield return delay;
            }
        }
    }
}