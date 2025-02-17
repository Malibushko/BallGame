using System;
using Infrastructure;
using UnityEngine;
using UniRx;

namespace Input
{
    public interface IPlayerInputService : IActivatableService
    {
        public Action<Vector3> OnInteractionBegin { get; set; }
        public Action<Vector3> OnInteractionChange { get; set; }
        public Action<Vector3> OnInteractionEnd { get; set; }
    }
}