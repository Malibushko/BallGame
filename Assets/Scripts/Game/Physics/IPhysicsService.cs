using System;
using UnityEngine;
using Zenject;

namespace Game.Physics
{
    public interface IPhysicsService : ITickable, IDisposable
    {
        public void Register(IPhysicsObject physicsObject);
        public void Unregister(IPhysicsObject physicsObject);
        
        public void ApplyForce(IPhysicsObject physicsObject, Vector3 force);
    }
}