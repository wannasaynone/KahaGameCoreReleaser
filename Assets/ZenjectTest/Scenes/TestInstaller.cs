using Zenject;

public class TestInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // KahaGameCore
        Container.Bind<KahaGameCore.Main.Core>().AsSingle();
        Container.Bind<KahaGameCore.Common.IJsonReader>().To<KahaGameCore.Common.GameStaticDataDeserializer>().AsTransient();
        Container.Bind<KahaGameCore.Common.IJsonWriter>().To<KahaGameCore.Common.GameStaticDataSerializer>().AsTransient();
        Container.Bind<KahaGameCore.Common.GameStaticDataManager>().AsSingle();
        Container.Bind<IInitializable>().To<Greeter>().AsSingle();
    }
}