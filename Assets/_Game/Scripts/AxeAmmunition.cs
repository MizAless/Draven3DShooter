using UnityEngine;

namespace _Game.Scripts
{
    public class AxeAmmunition : MonoBehaviour
    {
        private const int _maxCount = 2;

        public int CurrentCount { get; private set; } = 0;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Add();
            }
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