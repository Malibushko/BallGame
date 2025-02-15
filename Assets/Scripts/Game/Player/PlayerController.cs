using System;
using Game.Physics;
using Input;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    [RequireComponent(typeof(PhysicsComponent))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        private IPlayerInputService _input;
        private PhysicsComponent _physics;
        
        [Inject]
        public void Construct(IPlayerInputService input)
        {
            _input = input;
        }
        
        public void Enable()
        {
            _input.Enable();
            _input.OnInteractionBegin += OnInteractionBegin;
            _input.OnInteractionEnd += OnInteractionEnd;
        }
        
        public void Disable()
        {
            _input.Disable();
            _input.OnInteractionBegin -= OnInteractionBegin;
            _input.OnInteractionEnd -= OnInteractionEnd;
        }
        
        private void OnInteractionBegin()
        {
            
        }
        
        private void OnInteractionEnd()
        {
            if (_input.Movement.HasValue)
                _physics.ApplyForce(_input.Movement.Value);
        }
    }
}