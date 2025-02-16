using System;
using UniRx;
using Zenject;
using Vector2 = UnityEngine.Vector2;

namespace Input
{
    public class PlayerPointerInputService : IPlayerInputService
    {
        public ReactiveProperty<Vector2> Movement { get; } = new();
        
        public Action OnInteractionBegin { get; set; }
        public Action OnInteractionEnd { get; set; }
        
        private IInputService _inputService;
        
        private Vector2? _beginMousePosition;
        
        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;    
        }
        
        public void Activate()
        {
            if (_inputService != null)
            {
                _inputService.OnPointerPressed += OnPointerPressed;
                _inputService.OnPointerReleased += OnPointerReleased;
                _inputService.OnPointerMoved += OnPointerMoved;
            }
        }
        
        public void Deactivate()
        {
            if (_inputService != null)
            {
                _inputService.OnPointerPressed -= OnPointerPressed;
                _inputService.OnPointerReleased -= OnPointerReleased;
                _inputService.OnPointerMoved -= OnPointerMoved;
            }
        }
        
        private void OnPointerMoved(Vector2 position)
        {
            UpdateMovement(position);
        }
        
        private void OnPointerReleased(Vector2 position)
        {
            UpdateMovement(position);
            OnInteractionEnd?.Invoke();
            
            _beginMousePosition = null;
        }

        private void OnPointerPressed(Vector2 position)
        {
            _beginMousePosition = position;
            UpdateMovement(position);
            OnInteractionBegin?.Invoke();
        }
        
        private void UpdateMovement(Vector2 position)
        {
            if (_beginMousePosition != null)
            {
                var movement = position - _beginMousePosition.Value;
                Movement.Value = movement;
            }
        }
    }
}