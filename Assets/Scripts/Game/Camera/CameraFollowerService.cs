using Infrastructure;
using UnityEngine;
using Zenject;

namespace Game.Camera
{
    public class CameraFollowerService : IFixedTickable
    {
        private readonly ICameraService _camera;
        private readonly ITransformParametersProvider _target;
        private readonly Vector3 _positionOffset;
        private readonly Vector3 _rotationOffset;
        private readonly float _speed;
        
        [Inject]
        public CameraFollowerService(
            ICameraService camera, 
            ITransformParametersProvider target, 
            Vector3 positionOffset,
            Vector3 rotationOffset, 
            float speed)
        {
            _camera = camera;
            _target = target;
            _positionOffset = positionOffset;
            _rotationOffset = rotationOffset;
            _speed = speed;
        }
        
        public void FixedTick()
        {
            var camera = _camera.Camera;
            
            var position = _target.Position + _positionOffset;
            var smoothedPosition = Vector3.Lerp(camera.transform.position, position, _speed);
            camera.transform.position = smoothedPosition;
            
            camera.transform.LookAt(_target.Position);
            var rotation = _target.Rotation * Quaternion.Euler(_rotationOffset);
            var smoothedRotation = Quaternion.Slerp(camera.transform.rotation, rotation, _speed);
            camera.transform.rotation = smoothedRotation;
        }
    }
}