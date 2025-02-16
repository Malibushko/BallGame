using System;
using Infrastructure;
using UnityEngine;
using UniRx;

namespace Input
{
    public interface IPlayerInputService : IActivatableService
    {
        public ReactiveProperty<Vector2> Movement { get; }
        
        public Action OnInteractionBegin { get; set; }
        public Action OnInteractionEnd { get; set; }
    }
}