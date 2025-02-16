using System;
using Game.Physics;
using Infrastructure;
using Sirenix.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour, IPlayer
    {
        private IPhysicsObject _physicsObject;
        private IActivatableService[] _services;
        private Rigidbody _rigidbody;
        
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        
        [Inject]
        public void Construct(IActivatableService[] services, IPhysicsObject physicsObject)
        {
            _services = services;
            _physicsObject = physicsObject;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            _physicsObject.Configure(_rigidbody);
        }

        private void OnEnable()
        {
            _services.ForEach(service => service.Activate());
        }

        private void OnDisable()
        {
            _services.ForEach(service => service.Deactivate());
        }

        private void OnCollisionEnter(Collision collision)
        {
            _physicsObject.CollisionEnter?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            _physicsObject.CollisionExit?.Invoke(collision);
        }
    }
}