using System;
using UnityEngine;

public interface IInputService
{
    public Action<Vector2> OnPointerPressed { get; set; }
    public Action<Vector2> OnPointerReleased {get; set; }
    public Action<Vector2> OnPointerMoved { get; set; }
    
    public Action<KeyCode> OnKeyPressed { get; set; }
    public Action<KeyCode> OnKeyReleased { get; set; }
    
    public void TrackKey(KeyCode keyCode);
    public void UntrackKey(KeyCode keyCode);
}
