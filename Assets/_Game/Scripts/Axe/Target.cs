using System;
using UnityEngine;

namespace _Game.Scripts
{
    [SelectionBase]
    public class Target : MonoBehaviour
    {
        public void Hit()
        {
            Hited?.Invoke();
        }
        
        public event Action Hited;
    }
}