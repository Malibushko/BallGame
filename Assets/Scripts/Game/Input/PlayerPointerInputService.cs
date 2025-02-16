using System;
using Game.Camera;
using UniRx;
using UnityEngine;
using Zenject;
using Vector2 = UnityEngine.Vector2;

namespace Input
{
    public class PlayerPointerInputService : IPlayerInputService
    {
        public ReactiveProperty<Vector3> Movement { get; } = new();
        
        public Action<Vector3> OnInteractionBegin { get; set; }
        public Action<Vector3> OnInteractionEnd { get; set; }
        
        private IInputService _inputService;
        private ICameraService _cameraService;
        
        private Vector3? _beginMousePosition;
        
        [Inject]
        public void Construct(IInputService inputService, ICameraService cameraService)
        {
            _inputService = inputService;    
            _cameraService = cameraService;
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
            if (_beginMousePosition.HasValue)
                OnInteractionEnd?.Invoke(_beginMousePosition.Value + Movement.Value);
            _beginMousePosition = null;
        }

        private void OnPointerPressed(Vector2 position)
        {
            UpdateMovement(position);
            if (_beginMousePosition.HasValue)
                OnInteractionBegin?.Invoke(_beginMousePosition.Value);
        }
        
        private void UpdateMovement(Vector2 position)
        {
            if (TryGetWorldPosition(position, out Vector3 worldPos))
            {
                if (_beginMousePosition.HasValue)
                {
                    Movement.Value = worldPos - _beginMousePosition.Value;
                }
                else
                {
                    _beginMousePosition = worldPos;
                    Movement.Value = Vector3.zero;
                }
            }
        }

        private bool TryGetWorldPosition(Vector2 position, out Vector3 worldPosition)
        {
            var ray = _cameraService.Camera.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                worldPosition = Vector3.ProjectOnPlane(hit.point, Vector3.up);
                return true;
            }
            
            worldPosition = Vector3.zero;
            return false;
        }
    }
}