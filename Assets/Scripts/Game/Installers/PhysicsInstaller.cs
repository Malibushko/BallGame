using Game.Physics;
using Zenject;

namespace Game.Installers
{
    public class PhysicsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<PhysicsService>()
                .AsSingle()
                .NonLazy();
        }
    }
}