using Game.Camera;
using Game.Player;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class CameraFollowerInstaller : MonoInstaller
    {
        [SerializeField] private Vector3 _positionOffset = new(0f, 0f, 0f);
        [SerializeField] private Vector3 _rotationOffset = new(0f, 0f, 0f);
        [SerializeField] private float _speed =  1f;   
        
        public override void InstallBindings()
        {
            var player = Container.Resolve<IPlayer>();

            Container.BindInterfacesAndSelfTo<CameraFollowerService>()
                .AsSingle()
                .WithArguments(player, _positionOffset, _rotationOffset, _speed);
        }
    }
}