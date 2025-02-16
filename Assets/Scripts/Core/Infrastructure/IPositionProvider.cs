using UnityEngine;

namespace Infrastructure
{
    public interface IPositionProvider
    {
        public Vector3 Position { get; }
    }
}