using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace Input
{
    public class PlayerKeyboardInputService : IPlayerInputService
    {
        public ReactiveProperty<Vector3> Movement { get; } = new();
        
        public Action<Vector3> OnInteractionBegin { get; set; }
        public Action<Vector3> OnInteractionEnd { get; set; }
        
        private IInputService _inputService;
        private Config _config;
        private bool _isMoving;
        
        public class Config
        {
            public (KeyCode Left, KeyCode Right) HorizontalAxisKeys = (KeyCode.A, KeyCode.D);
            public (KeyCode Top, KeyCode Bottom) VerticalAxisKeys = (KeyCode.W, KeyCode.S);
            
            public IEnumerable<KeyCode> Keys
            {
                get
                {
                    yield return HorizontalAxisKeys.Left;
                    yield return HorizontalAxisKeys.Right;
                    yield return VerticalAxisKeys.Top;
                    yield return VerticalAxisKeys.Bottom;
                }
            }
        }
        
        [Inject]
        public void Construct(IInputService inputService, Config config)
        {
            _inputService = inputService; 
            _config = config;
        }
        
        public void Activate()
        {
            if (_inputService != null)
            {
                _inputService.OnKeyPressed += OnKeyPressed;
                _inputService.OnKeyReleased += OnKeyReleased;

                foreach (var key in _config.Keys)
                    _inputService.TrackKey(key);
            }
        }
        
        public void Deactivate()
        {
            if (_inputService != null)
            {
                _inputService.OnKeyPressed -= OnKeyPressed;
                _inputService.OnKeyReleased -= OnKeyReleased;
                
                foreach (var key in _config.Keys)
                    _inputService.UntrackKey(key);
            }
        }
        
        private void OnKeyPressed(KeyCode key)
        {
            var isMoving = IsMovingKey(key);
            
            if (isMoving && !_isMoving)
            {
                _isMoving = true;
                OnInteractionBegin?.Invoke(Vector3.zero);
            }
        }

        private void OnKeyReleased(KeyCode key)
        {
            var isMoving = IsMovingKey(key);

            Vector2 movement = Vector2.zero;
            
            if (key == _config.HorizontalAxisKeys.Left)
                movement = Vector2.left;
            else if (key == _config.HorizontalAxisKeys.Right)
                movement = Vector2.right;
            else if (key == _config.VerticalAxisKeys.Top)
                movement = Vector2.up;
            else if (key == _config.VerticalAxisKeys.Bottom)
                movement = Vector2.down;
            
            Movement.Value += new Vector3(movement.x, 0, movement.y);
            
            if (!isMoving && _isMoving)
            {
                _isMoving = false;
                OnInteractionEnd?.Invoke(Vector3.zero);
            }
        }

        private bool IsMovingKey(KeyCode key)
        {
            return _config.Keys.Contains(key);
        }
    }
}