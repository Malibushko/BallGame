using System;
using Game.Common.StateMachine;
using UnityEngine;

namespace Game.Player
{
    public class PlayerStateService : IPlayerStateService
    {
        public IState State { get; }
        public Action<IState> OnStateChange { get; }
    }
}