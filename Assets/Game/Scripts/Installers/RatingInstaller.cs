using UnityEngine;
using Zenject;

public class RatingInstaller : MonoInstaller
{
    [SerializeField] RatingAddView _ratingAddView;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<RatingAddController>().AsSingle();
        Container.BindInstance(_ratingAddView);
    }
}