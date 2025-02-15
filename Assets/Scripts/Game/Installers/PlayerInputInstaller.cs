using Input;
using Zenject;

namespace Game.Installers
{
    public class PlayerInputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerInputService>().To<PlayerPointerInputService>().AsSingle();
        }
    }
}