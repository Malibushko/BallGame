using Game.Camera;
using Game.Player;
using Zenject;

namespace Game.Installers
{
    public class PlayerCameraInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var playerMovement = Container.Resolve<IPlayerMovementService>();
            var cameraFollowerService = Container.Resolve<ICameraFollowerService>();
            
            cameraFollowerService.SetTarget(playerMovement);
        }
    }
}