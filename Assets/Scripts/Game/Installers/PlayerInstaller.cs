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
            
            bool playerControllerConfigLoaded = configs.Load(_playerControllerConfigPath, out PlayerControllerConfig playerControllerConfig);
            Assert.IsTrue(playerControllerConfigLoaded);

            Container.BindInterfacesAndSelfTo<PhysicsObject>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPhysicsMovementService>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<PlayerPointerInputService>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerController>()
                .AsSingle()
                .WithArguments(playerControllerConfig);
        }
    }
}