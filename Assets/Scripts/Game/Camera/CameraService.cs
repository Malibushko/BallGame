namespace Game.Camera
{
    public class CameraService : ICameraService
    {
        public UnityEngine.Camera Camera { get; }

        public CameraService(UnityEngine.Camera camera)
        {
            Camera = camera;
        }
    }
}