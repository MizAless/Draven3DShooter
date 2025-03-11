using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class AxeAmmunition : MonoBehaviour
    {
        private const int MaxCount = 2;

        public int CurrentCount { get; private set; } = 0;
        
        public static AxeAmmunition Instance { get; private set; }
        
        public event Action Changed;

        private void Start()
        {
            Instance = this;
        }
        
        public bool TryGet()
        {
            if (CurrentCount == 0)
                return false;

            CurrentCount--;
            
            Changed?.Invoke();
            return true;
        }

        public void Add()
        {
            CurrentCount++;

            if (CurrentCount > MaxCount)
                CurrentCount = MaxCount;
            else
                Changed?.Invoke();
        }
    }
}