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
            var player = Container.InstantiatePrefabForComponent<Player.Player>(_playerPrefab);
            player.transform.position = _spawnPoint.position;
            
            Container
                .Bind<IPlayer>()
                .To<Player.Player>()
                .FromInstance(player)
                .AsSingle();
        }
    }
}