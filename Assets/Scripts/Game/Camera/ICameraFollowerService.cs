using Infrastructure;

namespace Game.Camera
{
    public interface ICameraFollowerService
    {
        void SetTarget(ITransformParametersProvider target);
    }
}