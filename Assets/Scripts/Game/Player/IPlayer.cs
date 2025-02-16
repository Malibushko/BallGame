using UnityEngine;

namespace Game.Player
{
    public interface IPlayer
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Vector3 Velocity { get; }
    }
}