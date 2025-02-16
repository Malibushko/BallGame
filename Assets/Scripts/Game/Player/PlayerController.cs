using Input;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerController : IPlayerController
    {
        private IPlayerInputService _input;
        private IPlayerMovementService _movement;
        private PlayerControllerConfig _config;
        
        private bool _isInteracting;
        
        [Inject]
        public void Construct(IPlayerInputService input, 
            IPlayerMovementService movement,
            PlayerControllerConfig config)
        {
            _input = input;
            _movement = movement;
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
            float distanceToPlayer = Vector3.Distance(position, _movement.Position);
            
            if (distanceToPlayer < _config.MaxInteractionDistance)
                _isInteracting = true;
        }
        
        private void OnInteractionEnd(Vector3 position)
        {
            if (_isInteracting && _input.Movement.HasValue)
            {
                var movement = _input.Movement.Value * _config.Speed;
                _movement.Move(movement);
            }
        }
    }
}