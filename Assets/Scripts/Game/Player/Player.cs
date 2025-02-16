using System;
using Game.Physics;
using Infrastructure;
using Sirenix.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class Player : MonoBehaviour, IPlayer
    {
        public Vector3 Position => _physicsObject.Rigidbody.position;
        public Quaternion Rotation => _physicsObject.Rigidbody.rotation;
        public Vector3 Velocity => _physicsObject.Rigidbody.velocity;
        
        private IPhysicsObject _physicsObject;
        private IActivatableService[] _services;
        
        [Inject]
        public void Construct(IActivatableService[] activatableServices, IPhysicsObject physicsObject)
        {
            _services = activatableServices;
            _physicsObject = physicsObject;
        }
        
        private void OnEnable()
        {
            _services.ForEach(service => service.Activate());
        }

        private void OnDisable()
        {
            _services.ForEach(service => service.Deactivate());
        }
    }
}