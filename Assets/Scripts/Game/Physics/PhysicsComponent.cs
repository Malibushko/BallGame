using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Physics
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsComponent : MonoBehaviour
    {
        [SerializeField] private bool _isStatic;
        [HideIf("_isStatic")]
        [Tooltip("Defines bounciness for object; [0 is unelastic, 1 is fully elastic object]")]
        [Range(0, 1)]
        [SerializeField] private float _restitution;
        
        private IPhysicsObject _physicsObject;
        private Rigidbody _rigidbody;
        
        [Inject]
        public void Construct(IPhysicsObject physicsObject)
        {
            _physicsObject = physicsObject;
            _physicsObject.IsStatic = _isStatic;
            _physicsObject.Restitution = _restitution;
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