using Game.Player;
using Input;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class PlayerLocationInstaller : MonoInstaller
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _playerPrefab;

        public override void InstallBindings()
        {           
            var playerController = Container.InstantiatePrefabForComponent<PlayerController>(_playerPrefab);
            playerController.transform.position = _spawnPoint.position;
            
            Container
                .Bind<PlayerController>()
                .FromInstance(playerController)
                .AsSingle();
        }
    }
}