using System;
using UnityEngine;
using Zenject;

namespace Game.Physics
{
    public class PhysicsObject : IPhysicsObject
    {
        private IPhysicsService _physicsService;
        private Rigidbody _rigidBody;
        private Transform _transform;
        
        public float Restitution { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; set; }
        public Vector3 Position {
            get => Rigidbody ? _rigidBody.position : _transform.position;
            set
            {
                if (_rigidBody)
                    _rigidBody.MovePosition(value);
                else 
                    _transform.position = value;
            }
        }

        public Rigidbody Rigidbody => _rigidBody;
        public bool IsStatic { get; set; }
        
        public Action<Collision> CollisionEnter { get; set; }
        public Action<Collision> CollisionExit { get; set; }

        [Inject]
        public void Construct(IPhysicsService physics)
        {
            _physicsService = physics;
        }
        
        public void ApplyForce(Vector3 force)
        {
            _physicsService.ApplyForce(this, force);
        }

        public void Configure(GameObject owner)
        {
            _transform = owner.transform;
            _rigidBody = owner.GetComponent<Rigidbody>();
        }

        public void Activate()
        {
            _physicsService?.Register(this);
        }

        public void Deactivate()
        {
            _physicsService?.Unregister(this);
        }
    }
}