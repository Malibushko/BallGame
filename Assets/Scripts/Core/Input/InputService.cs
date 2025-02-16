using System;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Input
{
    public class InputService : IInputService, ITickable
    {
        private const int MainMouseButtonIndex = 0;
        
        public Action<Vector2> OnPointerPressed { get; set; }
        public Action<Vector2> OnPointerReleased { get; set; }
        public Action<Vector2> OnPointerMoved { get; set; }
        
        public Action<KeyCode> OnKeyPressed { get; set; }
        public Action<KeyCode> OnKeyReleased { get; set; }
        
        private Vector2? _lastPointerPosition;
        
        private List<MutableKeyValuePair<KeyCode, bool>> _trackedKeysStates = new();
        private HashSet<KeyCode> _trackedKeys = new();
        
        public void TrackKey(KeyCode keyCode)
        {
            if (_trackedKeys.Contains(keyCode))
            {
                _trackedKeys.Add(keyCode);
                _trackedKeysStates.Add(new MutableKeyValuePair<KeyCode, bool>(keyCode, false));
            }
        }

        public void UntrackKey(KeyCode keyCode)
        {
            if (_trackedKeys.Remove(keyCode))
                _trackedKeysStates.RemoveAll(x => x.Key == keyCode);
        }
        
        public void Tick()
        {
            ProcessPointerInput();
            ProcessKeyboardInput();
        }

        private void ProcessKeyboardInput()
        {
            foreach (var key in _trackedKeysStates)
            {
                var wasDown = key.Value;
                var isDown = UnityEngine.Input.GetKey(key.Key);

                if (wasDown != isDown)
                {
                    if (isDown)
                        OnKeyPressed?.Invoke(key.Key);
                    else
                        OnKeyReleased?.Invoke(key.Key);
                }
                
                key.Value = isDown;
            }
        }
        
        private void ProcessPointerInput()
        {
            if (UnityEngine.Input.GetMouseButton(MainMouseButtonIndex))
            {
                var currentMousePosition = UnityEngine.Input.mousePosition;
                
                if (!_lastPointerPosition.HasValue)
                {
                    _lastPointerPosition = currentMousePosition;
                    if (_lastPointerPosition != null) 
                        OnPointerPressed?.Invoke(currentMousePosition);
                } else
                {
                    if (currentMousePosition != _lastPointerPosition)
                    {
                        _lastPointerPosition = currentMousePosition;
                        OnPointerMoved?.Invoke(currentMousePosition);
                    }
                }
            }
            else
            {
                if (_lastPointerPosition.HasValue)
                {
                    OnPointerReleased?.Invoke(_lastPointerPosition.Value);
                    _lastPointerPosition = null;
                }
            }
        }
    }
}