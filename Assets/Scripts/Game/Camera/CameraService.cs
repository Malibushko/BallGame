using System;
using UnityEngine;
using Zenject;

namespace Game.Camera
{
    public class CameraService : ICameraService, ITickable
    {
        public Action OnCameraPositionChanged { get; set; }
        public UnityEngine.Camera Camera { get; }
    
        private Vector3 _cameraPosition;
        
        public CameraService(UnityEngine.Camera camera)
        {
            Camera = camera;
        }

        public void Tick()
        {
            if (Camera.transform.position != _cameraPosition)
            {
                _cameraPosition = Camera.transform.position;
                OnCameraPositionChanged?.Invoke();
            }
        }
    }
}