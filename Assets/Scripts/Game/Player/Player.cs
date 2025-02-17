using System;
using Game.Physics;
using Infrastructure;
using Sirenix.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class Player : IPlayer
    {
        public Vector3 Position => _playerMovementService.Position;
        public Quaternion Rotation => _playerMovementService.Rotation;
        public Vector3 Velocity => _physicsObject.Velocity;
        
        public Action<Vector3> OnHit { get; set; }
        
        private IPlayerMovementService _playerMovementService;
        private IPhysicsObject _physicsObject;
        private IActivatableService[] _activatableService;
        
        [Inject]
        public void Construct(IActivatableService[] services, IPlayerMovementService playerMovementService, IPhysicsObject physicsObject)
        {
            _activatableService = services;
            _playerMovementService = playerMovementService;
            _physicsObject = physicsObject;
            
            _physicsObject.CollisionEnter += OnCollisionEnter;
        }

        private void OnCollisionEnter(Collision obj)
        {
            if (obj.contactCount > 0)
                OnHit?.Invoke(obj.GetContact(0).point);
        }

        public void Enable()
        {
            _activatableService.ForEach(x => x.Activate());
        }

        public void Disable()
        {
            _activatableService.ForEach(x => x.Deactivate());
        }
    }
}