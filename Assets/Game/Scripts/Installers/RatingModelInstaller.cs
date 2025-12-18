using Zenject;

public class RatingModelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<RatingModel>().AsSingle();
    }
}