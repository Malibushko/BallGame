using Infrastructure;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        private IActivatableService[] _services;
        
        [Inject]
        public void Construct(IActivatableService[] services)
        {
            _services = services;
        }
        
        private void OnEnable()
        {
            _services.ForEach(service => service.Activate());
        }

        private void OnDisable()
        {
            _services.ForEach(service => service.Deactivate());
        }
    }
}