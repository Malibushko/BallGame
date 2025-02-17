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
        public Action<Vector3> OnInteractionBegin { get; set; }
        
        public Action<Vector3> OnInteractionChange { get; set; }
        public Action<Vector3> OnInteractionEnd { get; set; }
        
        private IInputService _inputService;
        private ICameraService _cameraService;
        
        private Vector3 _currentPosition = Vector3.zero;
        private bool _isInteracting;
        
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
                _cameraService.OnCameraPositionChanged += OnCameraPositionChanged;
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
            if (_isInteracting)
            {
                UpdateCurrentPosition(position);
                OnInteractionChange?.Invoke(_currentPosition);
            }
        }
        
        private void OnPointerReleased(Vector2 position)
        {
            UpdateCurrentPosition(position);
            
            if (_isInteracting)
                OnInteractionEnd?.Invoke(_currentPosition);
            
            _isInteracting = false;
        }

        private void OnPointerPressed(Vector2 position)
        {
            if (!_isInteracting)
            {
                _isInteracting = true;
                UpdateCurrentPosition(position);
                OnInteractionBegin?.Invoke(_currentPosition);   
            }
        }
        
        private void UpdateCurrentPosition(Vector2 position)
        {
            if (TryGetWorldPosition(position, out Vector3 worldPos))
            {
                _currentPosition = worldPos;
            }
        }

        private void OnCameraPositionChanged()
        {
            if (_isInteracting && _inputService.PointerPosition.HasValue)
            {
                UpdateCurrentPosition(_inputService.PointerPosition.Value);
                OnInteractionChange?.Invoke(_currentPosition);
            }
        }
        
        private bool TryGetWorldPosition(Vector2 position, out Vector3 worldPosition)
        {
            var ray = _cameraService.Camera.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var worldPos = Vector3.ProjectOnPlane(hit.point, Vector3.up);
                worldPosition = new Vector3(worldPos.x, 0, worldPos.z);
                return true;
            }
            
            worldPosition = Vector3.zero;
            return false;
        }
    }
}