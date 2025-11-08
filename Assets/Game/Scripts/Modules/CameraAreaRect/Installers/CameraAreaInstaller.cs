using Zenject;

public class CameraAreaInstaller : Installer<CameraAreaInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<CameraAreaRect>().AsSingle();
    }
}