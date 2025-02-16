using Game.Player;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class PlayerSpawner : MonoInstaller
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _playerPrefab;

        public override void InstallBindings()
        {           
            var player = Container.InstantiatePrefabForComponent<PlayerBehaviour>(_playerPrefab);
            player.transform.position = _spawnPoint.position;
            
            Container
                .Bind<PlayerBehaviour>()
                .FromInstance(player)
                .AsSingle();
        }
    }
}