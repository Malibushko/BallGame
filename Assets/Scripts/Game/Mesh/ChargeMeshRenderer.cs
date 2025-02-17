using UnityEngine;

namespace Game.Mesh
{
    public abstract class ChargeMeshRenderer : MonoBehaviour
    {
        public abstract void OnChargeChange(Vector3 startPoint, Vector3 endPoint);
        public abstract void Reset();
    }
}