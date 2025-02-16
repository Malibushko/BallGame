using Game.Common;
using Game.Physics;
using Game.Player;
using Infrastructure;
using Input;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Game.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private string _playerControllerConfigPath;
        
        public override void InstallBindings()
        {
            IConfigsService configs = Container.Resolve<IConfigsService>();
            
            Container
                .Bind(typeof(IPlayerInputService), typeof(IActivatableService))
                .To<PlayerPointerInputService>()
                .AsSingle();
            
            Container.Bind(typeof(IPhysicsObject), typeof(IActivatableService))
                .To<PhysicsObject>()
                .AsSingle();
            
            bool playerControllerConfigLoaded = configs.Load(_playerControllerConfigPath, out PlayerControllerConfig playerControllerConfig);
            Assert.IsTrue(playerControllerConfigLoaded);
            
            Container.Bind(typeof(IPlayerController), typeof(IActivatableService))
                .To<PlayerController>()
                .AsSingle()
                .WithArguments(playerControllerConfig);
        }
    }
}