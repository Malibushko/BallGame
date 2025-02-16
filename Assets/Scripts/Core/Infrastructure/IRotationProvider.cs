using UnityEngine;

namespace Infrastructure
{
    public interface IRotationProvider
    {
        public Quaternion Rotation { get; }
    }
}