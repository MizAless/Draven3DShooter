using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform _target;
        [SerializeField] private float _sensitivity;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _minAngle;
        
        private Vector2 _inputDirection;
        private float _cameraXAngle;
        
        private void Update()
        {
            _inputDirection = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        private void LateUpdate()
        {
            _cameraXAngle += -1 * _inputDirection.y * Time.deltaTime * _sensitivity;
            _cameraXAngle = Mathf.Clamp(_cameraXAngle, _minAngle, _maxAngle);

            _mainCamera.transform.localRotation = Quaternion.Euler(_cameraXAngle, 0, 0);
            
            _target.rotation *= Quaternion.Euler(0, _inputDirection.x * _sensitivity * Time.deltaTime, 0);
        }
    }
}