using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class AxeAmmunition : MonoBehaviour
    {
        private const int _maxCount = 2;

        public int CurrentCount { get; private set; } = 0;
        
        public static AxeAmmunition Instance { get; private set; }

        private void Start()
        {
            Instance = this;
        }
        
        public bool TryGet()
        {
            if (CurrentCount == 0)
                return false;

            CurrentCount--;
            return true;
        }

        public void Add()
        {
            CurrentCount++;
            
            if (CurrentCount > _maxCount)
                CurrentCount = _maxCount;
        }
    }
}