using Game.Physics;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerPhysicsMovementService : IPlayerMovementService
    {
        private IPhysicsObject _physicsObject;

        public Vector3 Position => _physicsObject.Position;
        public Quaternion Rotation => _physicsObject.Rotation;

        [Inject]
        public void Construct(IPhysicsObject physicsObject)
        {
            _physicsObject = physicsObject;
        }
        
        public void Move(Vector3 movement)
        {
            _physicsObject.ApplyForce(movement);
        }
    }
}