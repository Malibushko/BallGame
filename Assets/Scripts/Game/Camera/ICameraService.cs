using System;

namespace Game.Camera
{
    public interface ICameraService
    {
        public Action OnCameraPositionChanged { get; set; }
        
        public UnityEngine.Camera Camera { get; }
    }
}