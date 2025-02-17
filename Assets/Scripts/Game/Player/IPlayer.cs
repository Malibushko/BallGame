using System;
using Infrastructure;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayer
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Vector3 Velocity { get; }
        
        public Action<Vector3> OnHit { get; set; }

        public void Enable();
        public void Disable();
    }
}