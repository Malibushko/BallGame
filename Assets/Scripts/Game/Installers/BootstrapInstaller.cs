using Game.Common;
using Zenject;

namespace Game.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IConfigsService>()
                .To<JsonConfigsService>()
                .AsSingle();
        }
    }
}