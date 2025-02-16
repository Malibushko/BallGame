using Game.Camera;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private UnityEngine.Camera _camera;

        public override void InstallBindings()
        {
            Container
                .Bind<ICameraService>()
                .To<CameraService>()
                .AsSingle()
                .WithArguments(_camera);
        }
    }
}