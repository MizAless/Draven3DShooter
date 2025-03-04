using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class EffectsCreator : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _embersPrefab;

        public static EffectsCreator Instance {get; private set;}

        private void Start()
        {
            Instance = this;
        }

        public void CreateEmbers(Vector3 position)
        {
             Instantiate(_embersPrefab, position, Quaternion.identity);
        }
    }
}