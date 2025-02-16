using Game.Physics;
using Game.Player;
using Infrastructure;
using Input;
using Zenject;

namespace Game.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind(typeof(IPlayerInputService), typeof(IActivatableService))
                .To<PlayerPointerInputService>()
                .AsSingle();
            
            Container.Bind(typeof(IPhysicsObject), typeof(IActivatableService))
                .To<PhysicsObject>()
                .AsSingle();
            
            Container.Bind(typeof(IPlayerController), typeof(IActivatableService))
                .To<PlayerController>()
                .AsSingle();
        }
    }
}