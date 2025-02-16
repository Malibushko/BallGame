using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Physics
{
    public class PhysicsComponent : MonoBehaviour
    {
        [HideIf("_isStatic")]
        [SerializeField] float _restitution = 0.5f;
        [SerializeField] bool _isStatic;
        
        private IPhysicsObject _physicsObject;

        [Inject]
        public void Construct(IPhysicsObject physicsObject)
        {
            _physicsObject = physicsObject;
            
            _physicsObject.Restitution = _restitution;
            _physicsObject.IsStatic = _isStatic;
            
            _physicsObject.Configure(gameObject);
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