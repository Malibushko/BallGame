using System;
using UnityEngine;
using Zenject;

namespace Game.Physics
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsComponent : MonoBehaviour
    {
        private IPhysicsObject _physicsObject;
        private Rigidbody _rigidbody;
        
        [Inject]
        public void Construct(IPhysicsObject physicsObject)
        {
            _physicsObject = physicsObject;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _physicsObject.Configure(_rigidbody);
        }

        private void OnCollisionEnter(Collision other)
        {
            _physicsObject.CollisionEnter?.Invoke(other);
        }

        private void OnCollisionExit(Collision other)
        {
            _physicsObject.CollisionExit?.Invoke(other);
        }
    }
}