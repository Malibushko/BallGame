using System;
using Input;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerController : IPlayerController, IPlayerChargeController
    {
        public IReadOnlyReactiveProperty<bool> IsCharging => _isCharging;
        public IReadOnlyReactiveProperty<Vector3> ChargeStartPosition => _chargeStartPosition;
        public IReadOnlyReactiveProperty<Vector3> ChargeEndPosition => _chargeEndPosition;

        private ReactiveProperty<bool> _isCharging = new(false);
        private ReactiveProperty<Vector3> _chargeStartPosition = new(Vector3.zero);
        private ReactiveProperty<Vector3> _chargeEndPosition = new(Vector3.zero);

        private IPlayerInputService _input;
        private IPlayerMovementService _movement;
        private PlayerControllerConfig _config;
        
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
            {
                _isCharging.Value = true;
                _chargeStartPosition.Value = position;
                _chargeEndPosition.Value = position;
                _input.OnInteractionChange += OnInteractionChange;
            }
        }

        private void OnInteractionChange(Vector3 position)
        {
            _chargeStartPosition.Value = _movement.Position;
            _chargeEndPosition.Value = position;
        }

        private void OnInteractionEnd(Vector3 position)
        {
            if (_isCharging.Value)
            {
                var movement = (_chargeEndPosition.Value - _chargeStartPosition.Value) * _config.Speed;
                _movement.Move(movement);
                
                _isCharging.Value = false;
            }
        }
    }
}