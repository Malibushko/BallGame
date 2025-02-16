using System;
using Game.Common.StateMachine;
using Infrastructure;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayerStateService
    {
        public IState State { get; }
        public Action<IState> OnStateChange { get; }
    }
}