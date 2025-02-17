using System;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayerChargeController
    {
        public IReadOnlyReactiveProperty<Vector3> ChargeStartPosition { get; }
        public IReadOnlyReactiveProperty<Vector3> ChargeEndPosition { get; }
        
        public IReadOnlyReactiveProperty<bool> IsCharging { get; }
    }
}