using UnityEngine;
using Zenject;
using static Game.Math.Math;

namespace Game.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private GameObject _hitPrefab;
        [SerializeField] private LayerMask _collisionLayerMask;
        
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
        
        private void OnPlayerHit(Collision collision)
        {
            // TODO: move to factory
            if (_hitPrefab != null && collision.contactCount > 0 && HasLayer(_collisionLayerMask, collision.gameObject.layer))
            {
                var contact = collision.GetContact(0);
                Instantiate(_hitPrefab, contact.point, Quaternion.identity);
            }
        }
    }
}