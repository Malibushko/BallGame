using Game.Camera;
using Game.Physics;
using Input;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerController : IPlayerController
    {
        private IPlayerInputService _input;
        private IPhysicsObject _physics;
        private PlayerControllerConfig _config;
        
        private bool _isInteracting;
        
        [Inject]
        public void Construct(IPlayerInputService input, 
            IPhysicsObject physics,
            PlayerControllerConfig config)
        {
            _input = input;
            _physics = physics;
            _config = config;
        }

        public void Activate()
        {
            _input.OnInteractionBegin += OnInteractionBegin;
            _input.OnInteractionEnd += OnInteractionEnd;
        }
        
        public void Deactivate()
        {
            _input.OnInteractionBegin -= OnInteractionBegin;
            _input.OnInteractionEnd -= OnInteractionEnd;
        }
        
        private void OnInteractionBegin(Vector3 position)
        {
            float distanceToPlayer = Vector3.Distance(position, _physics.Position);
            
            if (distanceToPlayer < _config.MaxInteractionDistance)
                _isInteracting = true;
        }
        
        private void OnInteractionEnd(Vector3 position)
        {
            if (_isInteracting && _input.Movement.HasValue)
            {
                var force = _input.Movement.Value * _config.Speed;
                _physics.ApplyForce(force);
            }
        }
    }
}