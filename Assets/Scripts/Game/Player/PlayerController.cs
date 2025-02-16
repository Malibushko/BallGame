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
        
        [Inject]
        public void Construct(IPlayerInputService input, IPhysicsObject physics)
        {
            _input = input;
            _physics = physics;
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
        
        private void OnInteractionBegin()
        {
            
        }
        
        private void OnInteractionEnd()
        {
            if (_input.Movement.HasValue)
                _physics.ApplyForce(_input.Movement.Value * 0.001f);
        }
    }
}