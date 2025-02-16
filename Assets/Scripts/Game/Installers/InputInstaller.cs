using Input;
using Zenject;

namespace Game.Installers
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<InputService>()
                .AsSingle();
        }
    }
}