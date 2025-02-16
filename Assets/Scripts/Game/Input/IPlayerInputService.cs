using System;
using Infrastructure;
using UnityEngine;
using UniRx;

namespace Input
{
    public interface IPlayerInputService : IActivatableService
    {
        public ReactiveProperty<Vector3> Movement { get; }
        
        public Action<Vector3> OnInteractionBegin { get; set; }
        public Action<Vector3> OnInteractionEnd { get; set; }
    }
}