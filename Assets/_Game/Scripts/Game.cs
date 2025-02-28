using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class Game : MonoBehaviour
    {
        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}