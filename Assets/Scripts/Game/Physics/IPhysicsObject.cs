using System;
using UnityEngine;

namespace Game.Physics
{
    public interface IPhysicsObject
    {
        public Vector3 Velocity { get; set;  }
        public Vector3 Acceleration { get; set; }
        public Vector3 Position { get; set; }
        public float Restitution { get; }
        
        public Rigidbody Rigidbody { get; }
        public bool IsStatic { get; }
        
        public Action<Collision> CollisionEnter { get; set; }
        public Action<Collision> CollisionExit { get; set; }
    }
}