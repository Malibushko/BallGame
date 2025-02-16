using Infrastructure;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayerMovementService : ITransformParametersProvider
    {
        public void Move(Vector3 movement);
    }
}