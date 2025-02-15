using System;
using UnityEngine;
using UniRx;

namespace Input
{
    public interface IPlayerInputService
    {
        public ReactiveProperty<Vector2> Movement { get; }
        
        public Action OnInteractionBegin { get; set; }
        public Action OnInteractionEnd { get; set; }

        public void Enable();
        public void Disable();
    }
}