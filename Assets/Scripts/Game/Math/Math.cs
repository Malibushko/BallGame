using UnityEngine;

namespace Game.Math
{
    public static class Math
    {
        public static bool HasLayer(LayerMask mask, int layer)
        {
            return (mask.value & (1 << layer)) != 0;
        }
    }
}