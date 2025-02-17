using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private GameObject _hitPrefab;
        
        private IPlayer _player;
        
        [Inject]
        public void Construct(IPlayer player)
        {
            _player = player;
            _player.OnHit += OnPlayerHit;
            _player.Enable();
        }
        
        private void OnDisable()
        {
            if (_player != null)
            {
                _player.OnHit -= OnPlayerHit;
                _player.Disable();
            }
        }
        
        private void OnPlayerHit(Vector3 obj)
        {
            // TODO: move to factory
            if (_hitPrefab != null)
                Instantiate(_hitPrefab, transform.position, Quaternion.identity);
        }
    }
}