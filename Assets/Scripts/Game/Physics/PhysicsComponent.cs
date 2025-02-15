using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Physics
{
    public class PhysicsComponent : MonoBehaviour, IPhysicsObject
    {
        [SerializeField] private bool _isStatic;
        [HideIf("_isStatic")]
        [SerializeField] private float _restitution = 0.5f;
        
        private IPhysicsService _physicsService;
        private Rigidbody _rigidBody;
        
        public float Restitution => _restitution;
        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; set; }
        public Vector3 Position {
            get => Rigidbody ? _rigidBody.position : transform.position;
            set
            {
                if (_rigidBody)
                    _rigidBody.MovePosition(value);
                else 
                    transform.position = value;
            }
        }

        public Rigidbody Rigidbody => _rigidBody;
        public bool IsStatic => _isStatic;
        
        public Action<Collision> CollisionEnter { get; set; }
        public Action<Collision> CollisionExit { get; set; }

        [Inject]
        public void Construct(IPhysicsService physics)
        {
            _physicsService = physics;
        }

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        public void ApplyForce(Vector3 force)
        {
            _physicsService.ApplyForce(this, force);
        }
        
        private void OnEnable()
        {
            _physicsService?.Register(this);
        }

        private void OnDisable()
        {
            _physicsService?.Unregister(this);
        }

        public void OnCollisionEnter(Collision other)
        {
            CollisionEnter?.Invoke(other);
        }

        public void OnCollisionExit(Collision other)
        {
            CollisionExit?.Invoke(other);
        }
    }
}